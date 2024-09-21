using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PostDarkness.NPCs
{

    public class EmperorOfSpaceTail : ModNPC
{
    public override void SetDefaults()
    {
        NPC.width = 34;
        NPC.height = 40;
        NPC.damage = 30;
        NPC.defense = 10;
        NPC.lifeMax = 50000;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
        NPC.value = 10000f;
        NPC.knockBackResist = 0.5f;
        NPC.aiStyle = 6; // Worm AI style
        AIType = NPCID.DiggerTail;
        AnimationType = NPCID.DiggerTail;
        NPC.noGravity = true;
        NPC.noTileCollide = true;
    }

    public override bool PreAI()
    {
        if (NPC.ai[3] > 0f)
        {
            NPC.realLife = NPC.whoAmI;
        }
        if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead)
        {
            NPC.TargetClosest(true);
        }
        if (Main.player[NPC.target].dead)
        {
            NPC.velocity.Y = NPC.velocity.Y - 0.1f;
            if (NPC.timeLeft > 10)
            {
                NPC.timeLeft = 10;
            }
            return false;
        }
        return true;
    }
}

    public class EmperorOfSpaceBody : ModNPC
    {
    public override void SetDefaults()
    {
        NPC.width = 34;
        NPC.height = 40;
        NPC.damage = 150;
        NPC.defense = 50;
        NPC.lifeMax = 50000;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
        NPC.value = 10000f;
        NPC.knockBackResist = 0.5f;
        NPC.aiStyle = 6; // Worm AI style
        AIType = NPCID.DiggerBody;
        AnimationType = NPCID.DiggerBody;
        NPC.noGravity = true;
        NPC.noTileCollide = true;
    }

    public override bool PreAI()
    {
        if (NPC.ai[3] > 0f)
        {
            NPC.realLife = (int)NPC.ai[3];
        }
        if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead)
        {
            NPC.TargetClosest(true);
        }
        if (Main.player[NPC.target].dead)
        {
            NPC.velocity.Y = NPC.velocity.Y - 0.1f;
            if (NPC.timeLeft > 10)
            {
                NPC.timeLeft = 10;
            }
            return false;
        }
        return true;
    }
    }
    
    public class EmperorOfSpace : ModNPC
    {
        private int stage = 1;
        private int dashTimer = 0;
        private int phaseTimer = 0;
        private bool isDashing = false;

        public override void SetDefaults()
        {
            NPC.width = 30;
            NPC.height = 45;
            NPC.damage = 250;
            NPC.defense = 35;
            NPC.lifeMax = 75000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 10000f;
            NPC.knockBackResist = 0.5f;
            NPC.aiStyle = 6; // Custom AI style
            NPC.boss = true;
            NPC.noTileCollide = true;
            AIType = NPCID.Worm;
            Music = MusicID.Boss2;
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target];
            phaseTimer++;

            if (stage == 1)
            {
                if (phaseTimer < 900) // 15 seconds
                {
                    // Stay 20 tiles below the player
                    Vector2 targetPosition = player.Center + new Vector2(0, 20 * 16);
                    NPC.velocity = (targetPosition - NPC.Center) * 0.1f;
                }
                else if (phaseTimer < 2100) // 20 seconds of dashing
                {
                    if (!isDashing)
                    {
                        isDashing = true;
                        dashTimer = 0;
                    }
                    DashTowardsPlayer(player);
                }
                else
                {
                    phaseTimer = 0;
                    isDashing = false;
                }

                if (NPC.life < NPC.lifeMax * 0.5)
                {
                    stage = 2;
                    NPC.defense = (int)(NPC.defense * 1.05);
                    NPC.damage = (int)(NPC.damage * 1.05);
                }
            }
            else if (stage == 2)
            {
                if (phaseTimer < 900) // 15 seconds
                {
                    // Stay 20 tiles below the player
                    Vector2 targetPosition = player.Center + new Vector2(0, 20 * 16);
                    NPC.velocity = (targetPosition - NPC.Center) * 0.1f;
                }
               else if (phaseTimer < 3300) // 40 seconds of dashing
                {
                    if (!isDashing)
                    {
                        isDashing = true;
                        dashTimer = 0;
                    }
                    DashTowardsPlayer(player);
                }
                else
                {
                    phaseTimer = 0;
                    isDashing = false;
                }
            }
        }

        private void DashTowardsPlayer(Player player)
        {
            dashTimer++;
            if (dashTimer % 60 == 0) // Dash every second
            {
                Vector2 dashDirection = (player.Center - NPC.Center).SafeNormalize(Vector2.UnitX);
                NPC.velocity = dashDirection * 20f;
            }
        }
        public override bool PreAI()
    {
        // Worm AI logic
        if (NPC.ai[3] > 0f)
        {
            NPC.realLife = (int)NPC.ai[3];
        }
        if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead)
        {
            NPC.TargetClosest(true);
        }
        if (Main.player[NPC.target].dead)
        {
            NPC.velocity.Y = NPC.velocity.Y - 0.1f;
            if (NPC.timeLeft > 10)
            {
                NPC.timeLeft = 10;
            }
            return false;
        }
        if (Main.netMode != NetmodeID.MultiplayerClient)
        {
            if (NPC.ai[0] == 0f && NPC.ai[1] == 0f && NPC.ai[2] == 0f)
            {
                NPC.ai[3] = (float)NPC.whoAmI;
                NPC.realLife = NPC.whoAmI;
                int latestNPC = NPC.whoAmI;
                for (int i = 0; i < 10; i++)
                {
                    latestNPC = NPC.NewNPC(NPC.GetSource_FromAI(), (int)(NPC.position.X + (float)(NPC.width / 2)), (int)(NPC.position.Y + (float)(NPC.height / 2)), ModContent.NPCType<EmperorOfSpaceBody>(), NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
                    Main.npc[latestNPC].ai[3] = (float)NPC.whoAmI;
                    Main.npc[latestNPC].realLife = NPC.whoAmI;
                    Main.npc[latestNPC].ai[1] = (float)latestNPC;
                    Main.npc[latestNPC].ai[2] = (float)latestNPC;
                }
                latestNPC = NPC.NewNPC(NPC.GetSource_FromAI(), (int)(NPC.position.X + (float)(NPC.width / 2)), (int)(NPC.position.Y + (float)(NPC.height / 2)), ModContent.NPCType<EmperorOfSpaceBody>(), NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
                Main.npc[latestNPC].ai[3] = (float)NPC.whoAmI;
                Main.npc[latestNPC].realLife = NPC.whoAmI;
                Main.npc[latestNPC].ai[1] = (float)latestNPC;
                Main.npc[latestNPC].ai[2] = (float)latestNPC;
            }
        }

        return true;
    }
}
}