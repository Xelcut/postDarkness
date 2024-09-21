using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.GameContent.ItemDropRules;

namespace PostDarkness.NPCs
{
    [AutoloadBossHead]
    
    public class EmperorOfTheUnderground : ModNPC
    {
        private float rotationSpeed = 0.05f;
        private int dashCooldown = 120;
        private int dashTimer = 0;
        private float orbitRadius = 220f; // Distance from the player
        private int shootCooldown = 30; // Cooldown for shooting projectiles
        private int shootTimer = 0;
        private bool enraged = false;
        private Vector2 dashVelocity;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 1; // Number of animation frames
        }

        public override void SetDefaults()
        {
            NPC.width = 160;
            NPC.height = 160;
            NPC.damage = 150;
            NPC.defense = 50;
            NPC.lifeMax = 55000; // Health for post-Moon Lord
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 100000f;
            NPC.knockBackResist = 0.5f;
            NPC.aiStyle = -1; // Custom AI style
            NPC.boss = true;
            NPC.noTileCollide = true; // Allows the boss to go through walls
            Music = MusicID.Boss2;
        }

        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            // Only summon underground
            if (target.position.Y < Main.worldSurface * 16)
            {
                return false;
            }
            return true;

        }

        public override void AI()
        {

            Player player = Main.player[NPC.target];
            if (!player.active || player.dead)
            {
                NPC.TargetClosest(false);
                player = Main.player[NPC.target];
                if (!player.active || player.dead)
                {
                    NPC.velocity = new Vector2(0f, 10f);
                    if (NPC.timeLeft > 10)
                    {
                        NPC.timeLeft = 10;
                    }
                    return;
                }
            }

            if (NPC.Hitbox.Intersects(player.Hitbox))
            {
                player.Hurt(Terraria.DataStructures.PlayerDeathReason.ByNPC(NPC.whoAmI), NPC.damage, 50);
            }

            // Enrage if the player is on the surface
            if (player.position.Y < Main.worldSurface * 16)
            {
                if (!enraged)
                {
                    enraged = true;
                    NPC.damage *= 10;
                    NPC.defense *= 10;
                    rotationSpeed *= 10;
                    dashCooldown /= 10;
                }
            }
            else
            {
                if (enraged)
                {
                    enraged = false;
                    NPC.damage /= 10;
                    NPC.defense /= 10;
                    rotationSpeed /= 10;
                    dashCooldown *= 10;
                }
            }

            // Orbit around the player
            if (NPC.ai[1] == 0) // Only orbit if not dashing
            {
                Vector2 orbitPosition = player.Center + new Vector2(orbitRadius, 0).RotatedBy(NPC.ai[0]);
                Vector2 direction = orbitPosition - NPC.Center;
                NPC.velocity = direction * 0.1f; // Smooth movement towards the orbit position

                // Rotate around the player
                NPC.ai[0] += rotationSpeed;
            }

            // Dash into the player
            dashTimer++;
            if (dashTimer >= dashCooldown && NPC.ai[1] == 0)
            {
                dashTimer = 0;
                Vector2 dashDirection = player.Center - NPC.Center;
                dashDirection.Normalize();
                dashVelocity = dashDirection * 20f;
                NPC.velocity = dashVelocity;
                NPC.ai[1] = 1; // Set dash state
                NPC.ai[2] = 30; // Set dash duration
            }

            // Handle dash state
            if (NPC.ai[1] == 1)
            {
                NPC.ai[2]--;
                NPC.velocity = dashVelocity; // Maintain dash velocity
                if (NPC.ai[2] <= 0)
                {
                    NPC.ai[1] = 0; // Reset dash state
                    NPC.velocity *= 0.1f; // Slow down after dash
                }
            }

            // Shoot projectiles while spinning
            shootTimer++;
            if (shootTimer >= shootCooldown)
            {
                shootTimer = 0;
                Vector2 shootDirection = (player.Center - NPC.Center).RotatedByRandom(MathHelper.ToRadians(30));
                shootDirection.Normalize();
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, shootDirection * 10f, ProjectileID.Fireball, 50, 1f, Main.myPlayer);
            }

            // Increase speed as health depletes
            float healthPercentage = (float)NPC.life / NPC.lifeMax;
            rotationSpeed = 0.05f + (1f - healthPercentage) * 0.1f;
            dashCooldown = 120 - (int)((1f - healthPercentage) * 60);

            // Always face the player from the bottom
            Vector2 toPlayer = player.Center - NPC.Center;
            NPC.rotation = toPlayer.ToRotation() + MathHelper.PiOver2; // Adjust rotation to face the player from the bottom
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frame.Y = 0;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)    
        {
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<postDarkness.Items.Weapons.TerraniBlade>()));
            npcLoot.Add(ItemDropRule.Common(ItemID.SuperHealingPotion, 5, 10, 20));
        }
    }
}
