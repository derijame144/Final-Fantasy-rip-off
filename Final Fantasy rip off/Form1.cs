using Final_Fantasy_rip_off.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using static System.Windows.Forms.AxHost;

namespace Final_Fantasy_rip_off
{

    public partial class Form1 : Form
    {
        bool debugging = false;

        Image M1Sprite = null;
        Image M2Sprite = null;
        Image M3Sprite = null;
        Image sprite = Resources._7;
        Image darkKnightSprite = Resources.DK_Idle;
        Image blackMageSprite = Resources.BM_Idle;
        Image dragoonSprite = Resources.DR_Idle;
        Image spellSprite = null;

        Rectangle player = new Rectangle(359, 365, 20, 20);
        Rectangle darkKnight = new Rectangle(530, 220, 40, 40);
        Rectangle blackMage = new Rectangle(530, 170, 40, 40);
        Rectangle dragoon = new Rectangle(530, 270, 40, 40);

        Rectangle monsterPosition1 = new Rectangle(180, 180, 80, 80);
        Rectangle monsterPosition2 = new Rectangle(135, 90, 80, 80);
        Rectangle monsterPosition3 = new Rectangle(135, 267, 80, 80);

        Rectangle spellRectangle = new Rectangle(135, 267, 80, 80);

        Rectangle option1 = new Rectangle(530, 360, 100, 20);
        Rectangle option2 = new Rectangle(530, 390, 100, 20);
        Rectangle option3 = new Rectangle(530, 420, 100, 20);



        List<Rectangle> doors = new List<Rectangle>();
        List<Rectangle> wallsY = new List<Rectangle>();
        List<Rectangle> wallsX = new List<Rectangle>();

        List<int> speedList = new List<int>();

        string playerState = "idle";
        string gameState = "overWorld";
        string turn = "noOne";
        string animation = "";
        string menu = "start";

        string option1Text = "           Attack";
        string option2Text = "            Skill";
        string option3Text = "            Item";

        bool dKeyDown = false;
        bool aKeyDown = false;
        bool wKeyDown = false;
        bool sKeyDown = false;
        bool enterKeyDown = false;
        bool backKeyDown = false;
        bool buttonDown = false;

        bool wait = false;
        bool monsterTarget = false;
        bool all = false;
        bool charge = false;


        int spriteTimer = 0;
        int roomNumber = 1;
        int stepCount = 0;
        int stepReq;
        int option = 1;
        int animationPart = 1;
        int potions = 5;

        #region charater stats
        int dkMaxHP = 500;
        int dkCurrentHP = 500;
        int dkMaxSP = 50;
        int dkCurrentSP = 50;
        int dkCurrentSpeed = 3;
        int dkSetSpeed = 3;

        int bmMaxHP = 300;
        int bmCurrentHP = 300;
        int bmMaxSP = 100;
        int bmCurrentSP = 100;
        int bmCurrentSpeed = 5;
        int bmSetSpeed = 5;

        int drMaxHP = 400;
        int drCurrentHP = 400;
        int drMaxSP = 75;
        int drCurrentSP = 75;
        int drCurrentSpeed = 7;
        int drSetSpeed = 7;

        int M1HP;
        int M1Might;
        int M1Speed;
        int M1SetSpeed;
        int M2HP;
        int M2Might;
        int M2Speed;
        int M2SetSpeed;
        int M3HP;
        int M3Might;
        int M3Speed;
        int M3SetSpeed;
        #endregion

        int monsterNumber;
        int randVal;

        Font drawFont = new Font("Consolas", 30, FontStyle.Bold);

        Random rng = new Random();


        public Form1()
        {
            InitializeComponent();

            stepReq = rng.Next(6, 21);
        }

        public void turnOrder()
        {

            speedList.Clear();

            if (M1Speed == M2Speed || M1Speed == M3Speed)
            {
                M1Speed += 2;
            }

            if (M3Speed == M2Speed)
            {
                M2Speed += 1;
            }

            if (dkCurrentHP > 0)
            {
                speedList.Add(dkCurrentSpeed);
            }

            if (drCurrentHP > 0)
            {
                speedList.Add(drCurrentSpeed);
            }

            if (bmCurrentHP > 0)
            {
                speedList.Add(bmCurrentSpeed);
            }

            if (M1HP > 0 && M1Sprite != null)
            {
                speedList.Add(M1Speed);
            }

            if (M2HP > 0 && M2Sprite != null)
            {
                speedList.Add(M2Speed);
            }

            if (M3HP > 0 && M3Sprite != null)
            {
                speedList.Add(M3Speed);
            }

            speedList.Sort();
            speedList.Reverse();

        }

        public void turnAction()
        {
            if (turn == "noOne")
            {
                #region dead or low sprite
                if (M1HP < 0)
                {
                    M1Sprite = null;
                }

                if (M2HP < 0)
                {
                    M2Sprite = null;
                }

                if (M3HP < 0)
                {
                    M3Sprite = null;
                }

                if (drCurrentHP < 0)
                {
                    dragoonSprite = Resources.DR_Dead;
                }
                else if (drCurrentHP < drMaxHP / 4)
                {
                    dragoonSprite = Resources.DR_Low;
                }

                if (bmCurrentHP < 0)
                {
                    blackMageSprite = Resources.BM_Dead;
                }
                else if (bmCurrentHP < bmMaxHP / 4)
                {
                    blackMageSprite = Resources.BM_Low;
                }

                if (dkCurrentHP < 0)
                {
                    darkKnightSprite = Resources.DK_Dead;
                }
                else if (dkCurrentHP < dkMaxHP / 4)
                {
                    darkKnightSprite = Resources.DK_Low;
                }
                #endregion

                

                speedList.Sort();
                speedList.Reverse();

                menu = "start";

                if (speedList[0] <= 0)
                {
                    dkCurrentSpeed = dkSetSpeed;
                    drCurrentSpeed = drSetSpeed;
                    bmCurrentSpeed = bmSetSpeed;
                    M1Speed = M1SetSpeed;
                    M2Speed = M2SetSpeed;
                    M3Speed = M3SetSpeed;
                    turnOrder();
                }

                if (speedList[0] == dkCurrentSpeed)
                {
                    turn = "dk";
                }

                if (speedList[0] == drCurrentSpeed)
                {
                    turn = "dr";
                    animation = "drWalk";
                    option = 1;
                }

                if (speedList[0] == bmCurrentSpeed)
                {
                    turn = "bm";
                    animation = "bmWalk";
                    option = 1;
                }

                if (speedList[0] == M1Speed && M1Sprite != null)
                {
                    turn = "M1";
                }

                if (speedList[0] == M2Speed && M2Sprite != null)
                {
                    turn = "M2";
                }

                if (speedList[0] == M3Speed && M3Sprite != null)
                {
                    turn = "M3";
                }
            }

            if (wait == false)
            {
                if (turn == "M1")
                {

                    randVal = rng.Next(1, 4);

                    if (randVal == 1 && bmCurrentHP > 0)
                    {
                        bmCurrentHP -= M1Might;
                        animation = "bmHit";
                        speedList[0] -= 10;
                        M1Speed -= 10;
                    }

                    if (randVal == 2 && dkCurrentHP > 0)
                    {
                        dkCurrentHP -= M1Might;
                        animation = "dkHit";
                        speedList[0] -= 10;
                        M1Speed -= 10;
                    }

                    if (randVal == 3 && drCurrentHP > 0)
                    {
                        drCurrentHP -= M1Might;
                        animation = "drHit";
                        speedList[0] -= 10;
                        M1Speed -= 10;
                    }


                }

                if (turn == "M2")
                {

                    randVal = rng.Next(1, 4);

                    if (randVal == 1 && bmCurrentHP > 0)
                    {
                        bmCurrentHP -= M2Might;
                        animation = "bmHit";
                        speedList[0] -= 10;
                        M2Speed -= 10;
                    }

                    if (randVal == 2 && dkCurrentHP > 0)
                    {
                        dkCurrentHP -= M2Might;
                        animation = "dkHit";
                        speedList[0] -= 10;
                        M2Speed -= 10;
                    }

                    if (randVal == 3 && drCurrentHP > 0)
                    {
                        drCurrentHP -= M2Might;
                        animation = "drHit";
                        speedList[0] -= 10;
                        M2Speed -= 10;
                    }


                }

                if (turn == "M3")
                {

                    randVal = rng.Next(1, 4);

                    if (randVal == 1 && bmCurrentHP > 0)
                    {
                        bmCurrentHP -= M3Might;
                        animation = "bmHit";
                        speedList[0] -= 10;
                        M3Speed -= 10;
                    }

                    if (randVal == 2 && dkCurrentHP > 0)
                    {
                        dkCurrentHP -= M3Might;
                        animation = "dkHit";
                        speedList[0] -= 10;
                        M3Speed -= 10;
                    }

                    if (randVal == 3 && drCurrentHP > 0)
                    {
                        drCurrentHP -= M3Might;
                        animation = "drHit";
                        speedList[0] -= 10;
                        M3Speed -= 10;
                    }


                }

                if (buttonDown == false)
                {

                    if (turn == "dr")
                    {
                        if (enterKeyDown == true)
                        {

                            switch (option)
                            {
                                case 1:

                                    #region Attack select
                                    if (menu == "start")
                                    {
                                        menu = "drAttack";
                                        monsterTarget = true;
                                        buttonDown = true;
                                        break;
                                    }
                                    #endregion

                                    #region Jump select
                                    if (menu == "drSkills" && drCurrentSP >= 10)
                                    {
                                        menu = "Jump";
                                        monsterTarget = true;
                                        buttonDown = true;
                                        break;
                                    }
                                    #endregion

                                    #region attack M1
                                    if (menu == "drAttack" && M1Sprite != null)
                                    {
                                        animation = "drSwing";

                                        if (charge == true)
                                        {
                                            M1HP -= 60;
                                            charge = false;
                                        }
                                        else
                                        {
                                            M1HP -= 30;
                                        }

                                        buttonDown = true;
                                        monsterTarget = false;
                                        drCurrentSpeed -= 10;
                                        speedList[0] -= 10;
                                        break;
                                    }
                                    #endregion

                                    #region jump M1
                                    if (menu == "Jump" && M1Sprite != null)
                                    {
                                        animation = "Jump";

                                        if (charge == true)
                                        {
                                            M1HP -= 180;
                                            charge = false;
                                        }
                                        else
                                        {
                                            M1HP -= 60;
                                        }

                                        drCurrentSP -= 10;
                                        animationPart = 1;
                                        buttonDown = true;
                                        monsterTarget = false;
                                        drCurrentSpeed -= 10;
                                        speedList[0] -= 10;
                                        break;
                                    }
                                    #endregion

                                    #region wildswing attack
                                    if (menu == "wildSwing" && M1Sprite != null)
                                    {
                                        animation = "drSwing";

                                        if (charge == true)
                                        {
                                            M1HP -= 60;
                                            M2HP -= 60;
                                            M3HP -= 60;
                                            charge = false;
                                        }
                                        else
                                        {
                                            M1HP -= 30;
                                            M2HP -= 30;
                                            M3HP -= 30;
                                        }

                                        all = false;
                                        buttonDown = true;
                                        monsterTarget = false;
                                        drCurrentSP -= 15;
                                        drCurrentSpeed -= 10;
                                        speedList[0] -= 10;
                                        break;
                                    }
                                    #endregion

                                    #region charge
                                    if (menu == "charge")
                                    {
                                        animation = "charge";
                                        charge = true;
                                        buttonDown = true;
                                        drCurrentSP -= 20;
                                        drCurrentSpeed -= 10;
                                        speedList[0] -= 10;
                                    }
                                    #endregion

                                    break;
                                case 2:

                                    #region Skills menu
                                    if (menu == "start")
                                    {
                                        menu = "drSkills";
                                        buttonDown = true;
                                        break;
                                    }
                                    #endregion

                                    #region wildswing select
                                    if (menu == "drSkills" && drCurrentSP >= 15)
                                    {
                                        menu = "wildSwing";
                                        all = true;
                                        monsterTarget = true;
                                        buttonDown = true;
                                        break;
                                    }
                                    #endregion

                                    #region attack M2
                                    if (menu == "drAttack" && M2Sprite != null)
                                    {
                                        animation = "drSwing";

                                        if (charge == true)
                                        {
                                            M2HP -= 60;
                                            charge = false;
                                        }
                                        else
                                        {
                                            M2HP -= 30;
                                        }

                                        buttonDown = true;
                                        monsterTarget = false;
                                        drCurrentSpeed -= 10;
                                        speedList[0] -= 10;
                                        break;
                                    }
                                    #endregion

                                    #region jump M2
                                    if (menu == "Jump" && M2Sprite != null)
                                    {
                                        animation = "Jump";

                                        if (charge == true)
                                        {
                                            M2HP -= 180;
                                            charge = false;
                                        }
                                        else
                                        {
                                            M2HP -= 60;
                                        }

                                        drCurrentSP -= 10;
                                        animationPart = 1;
                                        buttonDown = true;
                                        monsterTarget = false;
                                        drCurrentSpeed -= 10;
                                        speedList[0] -= 10;
                                        break;
                                    }
                                    #endregion

                                    break;
                                case 3:

                                    #region charge select
                                    if (menu == "drSkills")
                                    {
                                        menu = "charge";
                                        buttonDown = true;
                                    }
                                    #endregion

                                    #region attack M3
                                    if (menu == "drAttack" && M3Sprite != null)
                                    {
                                        animation = "drSwing";

                                        if (charge == true)
                                        {
                                            M3HP -= 60;
                                            charge = false;
                                        }
                                        else
                                        {
                                            M3HP -= 30;
                                        }

                                        buttonDown = true;
                                        monsterTarget = false;
                                        drCurrentSpeed -= 10;
                                        speedList[0] -= 10;
                                    }
                                    #endregion

                                    #region jump M3
                                    if (menu == "Jump" && M3Sprite != null)
                                    {
                                        animation = "Jump";

                                        if (charge == true)
                                        {
                                            M3HP -= 180;
                                            charge = false;
                                        }
                                        else
                                        {
                                            M3HP -= 60;
                                        }

                                        drCurrentSP -= 10;
                                        animationPart = 1;
                                        buttonDown = true;
                                        monsterTarget = false;
                                        drCurrentSpeed -= 10;
                                        speedList[0] -= 10;
                                        break;
                                    }
                                    #endregion

                                    break;

                            }
                        }
                    }

                    if (turn == "bm")
                    {
                        if (enterKeyDown == true)
                        {
                            switch (option)
                            {
                                case 1:

                                    #region attack select
                                    if (menu == "start")
                                    {
                                        menu = "bmAttack";
                                        monsterTarget = true;
                                        buttonDown = true;
                                        break;
                                    }
                                    #endregion

                                    #region fire select
                                    if (menu == "bmSkills" && bmCurrentSP >= 10)
                                    {
                                        menu = "fire";
                                        monsterTarget = true;
                                        buttonDown = true;
                                        break;
                                    }
                                    #endregion

                                    #region fire M1
                                    if (menu == "fire" && M1Sprite != null)
                                    {
                                        animation = "fire";

                                        M1HP -= 80;
                                        buttonDown = true;
                                        monsterTarget = false;
                                        bmCurrentSpeed -= 10;
                                        speedList[0] -= 10;
                                        break;
                                    }
                                    #endregion



                                    break;

                                case 2:

                                    #region skills menu
                                    if (menu == "start")
                                    {
                                        menu = "bmSkills";
                                        buttonDown = true;
                                    }
                                    #endregion

                                    #region attack M2
                                    if (menu == "bmAttack" && M2Sprite != null)
                                    {
                                        animation = "bmSwing";

                                        M2HP -= 20;

                                        buttonDown = true;
                                        monsterTarget = false;
                                        bmCurrentSpeed -= 10;
                                        speedList[0] -= 10;
                                        break;
                                    }
                                    #endregion

                                    #region fire M2
                                    if (menu == "fire" && M2Sprite != null)
                                    {
                                        animation = "fire";

                                        M2HP -= 80;
                                        buttonDown = true;
                                        monsterTarget = false;
                                        bmCurrentSpeed -= 10;
                                        speedList[0] -= 10;
                                        break;
                                    }
                                    #endregion

                                    break;

                                case 3:

                                    #region attack M3
                                    if (menu == "bmAttack" && M3Sprite != null)
                                    {
                                        animation = "bmSwing";

                                        M3HP -= 20;

                                        buttonDown = true;
                                        monsterTarget = false;
                                        bmCurrentSpeed -= 10;
                                        speedList[0] -= 10;
                                        break;
                                    }
                                    #endregion

                                    #region fire M3
                                    if (menu == "fire" && M3Sprite != null)
                                    {
                                        animation = "fire";

                                        M3HP -= 80;
                                        buttonDown = true;
                                        monsterTarget = false;
                                        bmCurrentSpeed -= 10;
                                        speedList[0] -= 10;
                                        break;
                                    }
                                    #endregion

                                    break;
                            }
                        }
                    }
                }
            }



        }

        public void spriteChange()
        {
            if (dKeyDown == false && sKeyDown == false && wKeyDown == false && aKeyDown == false)
            {
                spriteTimer = 0;

                switch (playerState)
                {
                    case "up":
                        sprite = Resources._1;
                        break;
                    case "down":
                        sprite = Resources._7;
                        break;
                    case "left":
                        sprite = Resources._10;
                        break;
                    case "right":
                        sprite = Resources._4;
                        break;

                }
            }

            if (spriteTimer == 10 || spriteTimer == 19)
            {
                stepCount++;
            }

            if (wKeyDown == true)
            {
                spriteTimer++;
                playerState = "up";

                if (spriteTimer < 10)
                {
                    sprite = Resources._2;
                }
                else if (spriteTimer < 20)
                {
                    sprite = Resources._0;
                }
                else
                {
                    spriteTimer = 0;
                }
            }

            if (sKeyDown == true)
            {
                spriteTimer++;
                playerState = "down";

                if (spriteTimer < 10)
                {
                    sprite = Resources._8;
                }
                else if (spriteTimer < 20)
                {
                    sprite = Resources._6;
                }
                else
                {
                    spriteTimer = 0;
                }
            }

            if (aKeyDown == true)
            {
                spriteTimer++;
                playerState = "left";

                if (spriteTimer < 10)
                {
                    sprite = Resources._11;
                }
                else if (spriteTimer < 20)
                {
                    sprite = Resources._9;
                }
                else
                {
                    spriteTimer = 0;
                }
            }

            if (dKeyDown == true)
            {
                spriteTimer++;
                playerState = "right";

                if (spriteTimer < 10)
                {
                    sprite = Resources._3;
                }
                else if (spriteTimer < 20)
                {
                    sprite = Resources._5;
                }
                else
                {
                    spriteTimer = 0;
                }
            }
        }

        public void room()
        {

            wallsX.Clear();
            wallsY.Clear();
            doors.Clear();

            switch (roomNumber)
            {

                case 1:
                    BackgroundImage = Resources.Room_1;

                    #region Y walls
                    wallsY.Add(new Rectangle(355, 388, 27, 4));
                    wallsY.Add(new Rectangle(383, 380, 30, 4));
                    wallsY.Add(new Rectangle(327, 380, 23, 4));
                    wallsY.Add(new Rectangle(321, 294, 21, 4));
                    wallsY.Add(new Rectangle(298, 274, 27, 4));
                    wallsY.Add(new Rectangle(410, 274, 27, 4));
                    wallsY.Add(new Rectangle(396, 294, 21, 4));
                    wallsY.Add(new Rectangle(280, 254, 21, 4));
                    wallsY.Add(new Rectangle(437, 254, 21, 4));
                    wallsY.Add(new Rectangle(432, 220, 23, 4));
                    wallsY.Add(new Rectangle(278, 220, 23, 4));
                    wallsY.Add(new Rectangle(384, 198, 46, 4));
                    wallsY.Add(new Rectangle(303, 198, 48, 4));
                    wallsY.Add(new Rectangle(384, 160, 46, 4));
                    wallsY.Add(new Rectangle(305, 160, 46, 4));
                    wallsY.Add(new Rectangle(102, 113, 124, 4));
                    wallsY.Add(new Rectangle(228, 92, 100, 4));
                    wallsY.Add(new Rectangle(408, 92, 100, 4));
                    wallsY.Add(new Rectangle(330, 72, 100, 4));
                    wallsY.Add(new Rectangle(509, 113, 124, 4));
                    wallsY.Add(new Rectangle(509, 338, 124, 4));
                    wallsY.Add(new Rectangle(480, 318, 24, 4));
                    wallsY.Add(new Rectangle(480, 212, 24, 4));
                    wallsY.Add(new Rectangle(456, 192, 24, 4));
                    wallsY.Add(new Rectangle(430, 170, 24, 4));
                    wallsY.Add(new Rectangle(280, 170, 24, 4));
                    wallsY.Add(new Rectangle(256, 192, 24, 4));
                    wallsY.Add(new Rectangle(256, 192, 24, 4));
                    wallsY.Add(new Rectangle(230, 212, 26, 4));
                    wallsY.Add(new Rectangle(102, 170, 124, 4));
                    wallsY.Add(new Rectangle(535, 160, 70, 4));
                    wallsY.Add(new Rectangle(536, 242, 18, 4));
                    wallsY.Add(new Rectangle(587, 242, 18, 4));

                    #endregion

                    #region X walls
                    wallsX.Add(new Rectangle(351, 380, 4, 10));
                    wallsX.Add(new Rectangle(380, 380, 4, 10));
                    wallsX.Add(new Rectangle(341, 294, 4, 103));
                    wallsX.Add(new Rectangle(392, 294, 4, 103));
                    wallsX.Add(new Rectangle(325, 274, 4, 20));
                    wallsX.Add(new Rectangle(406, 274, 4, 20));
                    wallsX.Add(new Rectangle(432, 254, 4, 20));
                    wallsX.Add(new Rectangle(300, 254, 4, 20));
                    wallsX.Add(new Rectangle(458, 192, 4, 200));
                    wallsX.Add(new Rectangle(274, 192, 4, 200));
                    wallsX.Add(new Rectangle(300, 190, 4, 32));
                    wallsX.Add(new Rectangle(432, 190, 4, 32));
                    wallsX.Add(new Rectangle(382, 160, 4, 41));
                    wallsX.Add(new Rectangle(350, 160, 4, 41));
                    wallsX.Add(new Rectangle(533, 160, 4, 85));
                    wallsX.Add(new Rectangle(604, 160, 4, 85));
                    wallsX.Add(new Rectangle(552, 202, 4, 43));
                    wallsX.Add(new Rectangle(584, 202, 4, 43));
                    wallsX.Add(new Rectangle(633, 110, 4, 260));
                    wallsX.Add(new Rectangle(476, 196, 4, 260));
                    wallsX.Add(new Rectangle(500, 318, 4, 260));
                    wallsX.Add(new Rectangle(98, 117, 4, 260));
                    wallsX.Add(new Rectangle(224, 170, 4, 50));
                    wallsX.Add(new Rectangle(254, 193, 4, 50));
                    wallsX.Add(new Rectangle(280, 174, 4, 50));
                    wallsX.Add(new Rectangle(452, 174, 4, 50));
                    wallsX.Add(new Rectangle(507, 93, 4, 23));
                    wallsX.Add(new Rectangle(224, 93, 4, 23));
                    wallsX.Add(new Rectangle(405, 52, 4, 43));
                    wallsX.Add(new Rectangle(326, 52, 4, 43));
                    #endregion

                    doors.Add(new Rectangle(556, 202, 45, 1));

                    for (int i = 0; i < doors.Count; i++)
                    {
                        if (player.IntersectsWith(doors[i]))
                        {
                            roomNumber = 2;
                            player.Location = new Point(536, 212);
                        }
                    }

                    break;

                case 2:
                    BackgroundImage = Resources.Room_2;

                    wallsX.Add(new Rectangle(548, 212, 4, 20));

                    break;
            }


            for (int i = 0; i < wallsY.Count; i++)
            {
                if (player.IntersectsWith(wallsY[i]))
                {
                    if (player.Y < wallsY[i].Y)
                    {
                        player.Y = wallsY[i].Y - player.Height;
                    }
                    else if (player.Y > wallsY[i].Y)
                    {
                        player.Y = wallsY[i].Y + wallsY[i].Height;
                    }
                }
            }

            for (int i = 0; i < wallsX.Count; i++)
            {
                if (player.IntersectsWith(wallsX[i]))
                {
                    if (player.X < wallsX[i].X)
                    {
                        player.X = wallsX[i].X - player.Width;
                    }
                    else if (player.X > wallsX[i].X)
                    {
                        player.X = wallsX[i].X + wallsX[i].Width;
                    }
                }
            }

        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    if (gameState == "battle")
                    {
                        if (wKeyDown == false && wait == false)
                        {
                            switch (option)
                            {
                                case 1:
                                    option = 3;
                                    break;
                                case 2:
                                    option = 1;
                                    break;
                                case 3:
                                    option = 2;
                                    break;

                            }
                        }
                    }
                    wKeyDown = true;
                    break;
                case Keys.S:
                    if (gameState == "battle")
                    {
                        if (wKeyDown == false && wait == false)
                        {
                            switch (option)
                            {
                                case 1:
                                    option = 2;
                                    break;
                                case 2:
                                    option = 3;
                                    break;
                                case 3:
                                    option = 1;
                                    break;
                            }
                        }
                    }
                    sKeyDown = true;
                    break;
                case Keys.D:
                    dKeyDown = true;
                    break;
                case Keys.A:
                    aKeyDown = true;
                    break;
                case Keys.Enter:
                    enterKeyDown = true;
                    break;
                case Keys.Back:
                    backKeyDown = true;
                    break;

            }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wKeyDown = false;
                    break;
                case Keys.S:
                    sKeyDown = false;
                    break;
                case Keys.A:
                    aKeyDown = false;
                    break;
                case Keys.D:
                    dKeyDown = false;
                    break;
                case Keys.Enter:
                    enterKeyDown = false;
                    buttonDown = false;
                    break;
                case Keys.Back:
                    backKeyDown = false;
                    break;
            }
        }

        private void Gamer_Tick(object sender, EventArgs e)
        {

            if (gameState == "overWorld")
            {
                if (wKeyDown == true)
                {
                    player.Y -= 2;
                }

                if (sKeyDown == true)
                {
                    player.Y += 2;
                }

                if (dKeyDown == true)
                {
                    player.X += 2;
                }

                if (aKeyDown == true)
                {
                    player.X -= 2;
                }

                spriteChange();
                room();

                if (stepCount == stepReq)
                {
                    gameState = "battle";

                    spriteTimer = 0;

                    randVal = rng.Next(1, 5);

                    for (int i = 1; i <= randVal; i++)
                    {
                        switch (i)
                        {
                            case 1:
                                monsterNumber = rng.Next(1, 5);
                                if (monsterNumber == 1)
                                {
                                    M1Sprite = Resources.Monster1;
                                    M1HP = 150;
                                    M1Might = 20;
                                    M1Speed = 4;
                                    M1SetSpeed = 4;
                                }
                                else if (monsterNumber == 2)
                                {
                                    M1Sprite = Resources.Monster2;
                                    M1HP = 100;
                                    M1Might = 50;
                                    M1Speed = 6;
                                    M1SetSpeed = 6;
                                }
                                else if (monsterNumber == 3)
                                {
                                    M1Sprite = Resources.Monster3;
                                    M1HP = 50;
                                    M1Might = 10;
                                    M1Speed = 14;
                                    M1SetSpeed = 14;
                                }
                                else
                                {
                                    M1Sprite = Resources.Monster4;
                                    M1HP = 250;
                                    M1Might = 75;
                                    M1Speed = 1;
                                    M1SetSpeed = 1;
                                }
                                break;

                            case 2:
                                monsterNumber = rng.Next(1, 5);
                                if (monsterNumber == 1)
                                {
                                    M2Sprite = Resources.Monster1;
                                    M2HP = 400;
                                    M2Might = 20;
                                    M2Speed = 4;
                                    M2SetSpeed = 4;
                                }
                                else if (monsterNumber == 2)
                                {
                                    M2Sprite = Resources.Monster2;
                                    M2HP = 100;
                                    M2Might = 50;
                                    M2Speed = 6;
                                    M2SetSpeed = 6;
                                }
                                else if (monsterNumber == 3)
                                {
                                    M2Sprite = Resources.Monster3;
                                    M2HP = 50;
                                    M2Might = 10;
                                    M2Speed = 14;
                                    M2SetSpeed = 14;
                                }
                                else
                                {
                                    M2Sprite = Resources.Monster4;
                                    M2HP = 250;
                                    M2Might = 75;
                                    M2Speed = 1;
                                    M2SetSpeed = 1;
                                }
                                break;

                            case 3:
                                monsterNumber = rng.Next(1, 5);
                                if (monsterNumber == 1)
                                {
                                    M3Sprite = Resources.Monster1;
                                    M3HP = 400;
                                    M3Might = 20;
                                    M3Speed = 4;
                                    M3SetSpeed = 4;
                                }
                                else if (monsterNumber == 2)
                                {
                                    M3Sprite = Resources.Monster2;
                                    M3HP = 100;
                                    M3Might = 50;
                                    M3Speed = 6;
                                    M3SetSpeed = 6;
                                }
                                else if (monsterNumber == 3)
                                {
                                    M3Sprite = Resources.Monster3;
                                    M3HP = 50;
                                    M3Might = 10;
                                    M3Speed = 14;
                                    M3SetSpeed = 14;
                                }
                                else
                                {
                                    M3Sprite = Resources.Monster4;
                                    M3HP = 250;
                                    M3Might = 75;
                                    M3Speed = 1;
                                    M3SetSpeed = 1;
                                }
                                break;


                        }

                    }

                    BackgroundImage = Resources.battle_room;
                    menu = "start";

                    Thread.Sleep(20);
                    turnOrder();
                }
            }

            if (gameState == "battle")
            {
                turnAction();

                if (backKeyDown == true)
                {
                    if (menu != "start")
                    {
                        menu = "start";
                    }

                    all = false;
                    monsterTarget = false;
                }

                if (option == 1 && option1Text == "")
                {
                    option = 2;
                }

                if (option == 2 && option2Text == "")
                {
                    option = 3;
                }

                if (option == 3 && option3Text == "")
                {
                    option = 1;
                }

                if (menu == "start")
                {
                    option1Text = "           Attack";
                    option2Text = "            Skill";
                    option3Text = "            Item";
                }

                if (menu == "charge")
                {
                    option1Text = "  2x on next Hit";
                    option2Text = "";
                    option3Text = "";
                }

                if (all == true)
                {
                    option1Text = "          Hit All";
                    option2Text = "";
                    option3Text = "";
                }

                else if (monsterTarget == true)
                {
                    if (M1Sprite != null)
                    {
                        option1Text = "          Monster 1";
                    }
                    else
                    {
                        option1Text = "";
                    }

                    if (M2Sprite != null)
                    {
                        option2Text = "          Monster 2";
                    }
                    else
                    {
                        option2Text = "";
                    }

                    if (M3Sprite != null)
                    {
                        option3Text = "          Monster 3";
                    }
                    else
                    {
                        option3Text = "";
                    }
                }

                #region skill menus

                if (menu == "drSkills")
                {
                    option1Text = "       Jump  SP: 10";
                    option2Text = "Wild Swing SP: 15";

                    if (charge == false)
                    {
                        option3Text = "      Charge SP: 20";
                    }
                    else
                    {
                        option3Text = "       Charged";
                    }

                }

                if (menu == "bmSkills")
                {
                    option1Text = "       Fire  SP: 10";
                    option2Text = "      Quake  SP: 15";
                    option3Text = "       Fast  SP: 20";
                }

                #endregion

            }

            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (gameState == "overWorld")
            {
                e.Graphics.DrawImage(sprite, player);


            }

            if (gameState == "battle")
            {
                bmStatsLabel.Visible = true;
                dkStatsLabel.Visible = true;
                drStatsLabel.Visible = true;

                bmStatsLabel.Text = $"   Black Mage\n\n  HP: {bmCurrentHP}/{bmMaxHP}\n  SP: {bmCurrentSP}/{bmMaxSP}";
                dkStatsLabel.Text = $"   Dark Knight\n\n  HP: {dkCurrentHP}/{dkMaxHP}\n  SP: {dkCurrentSP}/{dkMaxSP}";
                drStatsLabel.Text = $"   Dragoon\n\n  HP: {drCurrentHP}/{drMaxHP}\n  SP: {drCurrentSP}/{drMaxSP}";
                XcordLabel.Text = turn + " " + animation;
                YcordLabel.Text = $"Timer: {spriteTimer}\n{speedList[0]} {speedList[1]} {speedList[2]}\nPart: {dragoon.X}";

               

                if (M1Sprite != null)
                {
                    e.Graphics.DrawImage(M1Sprite, monsterPosition1);

                    if (monsterTarget == true && option == 1 || all == true)
                    {
                        e.Graphics.DrawRectangle(Pens.White, monsterPosition1);
                    }
                }

                if (M2Sprite != null)
                {
                    e.Graphics.DrawImage(M2Sprite, monsterPosition2);

                    if (monsterTarget == true && option == 2 || all == true)
                    {
                        e.Graphics.DrawRectangle(Pens.White, monsterPosition2);
                    }
                }

                if (M3Sprite != null)
                {
                    e.Graphics.DrawImage(M3Sprite, monsterPosition3);

                    if (monsterTarget == true && option == 3 || all == true)
                    {
                        e.Graphics.DrawRectangle(Pens.White, monsterPosition3);
                    }
                }

                if (spellSprite != null)
                {
                    e.Graphics.DrawImage(spellSprite, spellRectangle);
                }

                e.Graphics.DrawImage(darkKnightSprite, darkKnight);
                e.Graphics.DrawImage(dragoonSprite, dragoon);
                e.Graphics.DrawImage(blackMageSprite, blackMage);

                e.Graphics.FillRectangle(Brushes.White, 26, 343, 636, 108);
                e.Graphics.FillRectangle(Brushes.DarkBlue, 30, 347, 628, 100);
                e.Graphics.DrawString(option1Text, Font, Brushes.White, option1);
                e.Graphics.DrawString(option2Text, Font, Brushes.White, option2);
                e.Graphics.DrawString(option3Text, Font, Brushes.White, option3);

                switch (option)
                {
                    case 1:
                        e.Graphics.DrawRectangle(Pens.White, option1);
                        break;
                    case 2:
                        e.Graphics.DrawRectangle(Pens.White, option2);
                        break;
                    case 3:
                        e.Graphics.DrawRectangle(Pens.White, option3);
                        break;
                }
            }

            if (debugging)
            {
                if (gameState == "overWorld")
                {
                    XcordLabel.Text = $"X:{player.X}";
                    YcordLabel.Text = $"Y:{player.Y}\nStep Count: {stepCount}\nStep Req: {stepReq}";

                    for (int i = 0; i < wallsY.Count; i++)
                    {
                        e.Graphics.DrawRectangle(Pens.AliceBlue, wallsY[i]);
                    }

                    for (int i = 0; i < wallsX.Count; i++)
                    {
                        e.Graphics.DrawRectangle(Pens.AliceBlue, wallsX[i]);
                    }

                    for (int i = 0; i < doors.Count; i++)
                    {
                        e.Graphics.DrawRectangle(Pens.Red, doors[i]);
                    }


                }


            }
        }

        private void Animation_Tick(object sender, EventArgs e)
        {


            if (gameState == "battle")
            {



                if (animation != "")
                {
                    wait = true;
                    spriteTimer += 1;
                }
                else
                {
                    wait = false;
                    spriteTimer = 0;
                    animationPart = 1;
                }

                #region Hit Animations
                if (animation == "bmHit")
                {
                    blackMageSprite = Resources.BM_Hit;

                    if (spriteTimer > 40)
                    {
                        blackMageSprite = Resources.BM_Idle;
                        animation = "";
                        turn = "noOne";
                        spriteTimer = 0;
                    }
                }

                if (animation == "drHit")
                {
                    dragoonSprite = Resources.DR_Hit;

                    if (spriteTimer > 40)
                    {
                        dragoonSprite = Resources.DR_Idle;
                        animation = "";
                        turn = "noOne";
                        spriteTimer = 0;
                    }
                }

                if (animation == "dkHit")
                {
                    darkKnightSprite = Resources.DK_Hit;

                    if (spriteTimer > 40)
                    {
                        darkKnightSprite = Resources.DK_Idle;
                        animation = "";
                        turn = "noOne";
                        spriteTimer = 0;
                    }
                }
                #endregion

                #region DR Animations
                switch (animation)
                {
                    case "drWalk":
                        if (dragoon.X > 430)
                        {
                            dragoonSprite = Resources.DR_Walk;
                            dragoon.X -= 10;
                        }

                        else
                        {
                            dragoon.X = 430;
                            dragoonSprite = Resources.DR_Idle;
                            animation = "";
                        }
                        break;

                    case "drSwing":
                        if (spriteTimer <= 5 || spriteTimer > 10)
                        {
                            dragoonSprite = Resources.DR_SwingUp;
                        }
                        else
                        {
                            dragoonSprite = Resources.DR_SwingDown;
                        }


                        if (spriteTimer > 15 && dragoon.X < 530)
                        {
                            dragoonSprite = Resources.DR_Walk;
                            dragoon.X += 10;
                        }
                        else if (dragoon.X >= 530)
                        {
                            dragoon.X = 530;
                            dragoonSprite = Resources.DR_Idle;
                            animation = "";
                            turn = "noOne";
                        }
                        break;

                    case "Jump":
                        if (dragoon.Y + dragoon.Height > -1 && animationPart == 1)
                        {
                            dragoon.Y -= 20;
                        }
                        else if (animationPart == 1)
                        {
                            animationPart = 2;
                            spriteTimer = 0;
                        }

                        if (animationPart == 2)
                        {
                            if (option == 1)
                            {
                                dragoon.X = 200;
                            }
                            else
                            {
                                dragoon.X = 155;
                            }

                            if (spriteTimer == 25)
                            {
                                animationPart = 3;
                            }
                        }

                        if (animationPart == 3)
                        {

                            dragoonSprite = Resources.DR_Jump;

                            switch (option)
                            {
                                case 1:
                                    if (dragoon.Y < 180)
                                    {
                                        dragoon.Y += 30;
                                    }
                                    else
                                    {
                                        dragoon.Y = 180;
                                        animationPart = 4;
                                        spriteTimer = 0;
                                    }
                                    break;
                                case 2:
                                    if (dragoon.Y < 90)
                                    {
                                        dragoon.Y += 30;
                                    }
                                    else
                                    {
                                        dragoon.Y = 90;
                                        animationPart = 4;
                                        spriteTimer = 0;
                                    }
                                    break;
                                case 3:
                                    if (dragoon.Y < 267)
                                    {
                                        dragoon.Y += 30;
                                    }
                                    else
                                    {
                                        dragoon.Y = 267;
                                        animationPart = 4;
                                        spriteTimer = 0;
                                    }
                                    break;
                            }
                        }

                        if (animationPart == 4)
                        {
                            if (spriteTimer == 25)
                            {
                                animationPart = 5;
                            }
                        }

                        if (animationPart == 5)
                        {
                            if (dragoon.Y + dragoon.Height > -1 && animationPart == 5)
                            {
                                dragoon.Y -= 20;
                            }
                            else if (animationPart == 5)
                            {
                                animationPart = 6;
                                spriteTimer = 0;
                            }
                        }

                        if (animationPart == 6)
                        {
                            if (spriteTimer == 25)
                            {
                                animationPart = 7;
                                dragoon.X = 530;
                                dragoonSprite = Resources.DR_Idle;
                            }
                        }

                        if (animationPart == 7)
                        {
                            if (dragoon.Y < 270)
                            {
                                dragoon.Y += 20;
                            }
                            else
                            {
                                dragoon.Y = 270;
                                animation = "";
                                turn = "noOne";
                            }
                        }

                        break;

                    case "charge":

                        if (spriteTimer < 25)
                        {
                            dragoonSprite = Resources.DR_Charge;
                        }
                        else if (dragoon.X < 530)
                        {
                            dragoonSprite = Resources.DR_Walk;
                            dragoon.X += 10;
                        }
                        else
                        {
                            dragoonSprite = Resources.DR_Idle;
                            animation = "";
                            turn = "noOne";
                        }

                        break;
                }




                #endregion

                switch (animation)
                {
                    case "bmWalk":

                        if (blackMage.X > 430)
                        {
                            blackMageSprite = Resources.BM_Walk;
                            blackMage.X -= 10;
                        }

                        else
                        {
                            blackMage.X = 430;
                            blackMageSprite = Resources.BM_Idle;
                            animation = "";
                        }
                        break;

                    case "bmSwing":

                        if (spriteTimer <= 5 || spriteTimer > 10)
                        {
                            blackMageSprite = Resources.BM_SwingUp;
                        }
                        else
                        {
                            blackMageSprite = Resources.BM_SwingDown;
                        }


                        if (spriteTimer > 15 && blackMage.X < 530)
                        {
                            blackMageSprite = Resources.BM_Walk;
                            blackMage.X += 10;
                        }
                        else if (blackMage.X >= 530)
                        {
                            blackMage.X = 530;
                            blackMageSprite = Resources.BM_Idle;
                            animation = "";
                            turn = "noOne";
                        }

                        break;

                    case "fire":

                        if (option == 1)
                        {
                            spellRectangle = monsterPosition1;
                        }
                        else if (option == 2)
                        {
                            spellRectangle = monsterPosition2;
                        }
                        else
                        {
                            spellRectangle = monsterPosition3;
                        }

                        if (spriteTimer % 2 == 0)
                        {
                            blackMageSprite = Resources.BM_ChantStart;
                        }
                        else
                        {
                            blackMageSprite = Resources.BM_ChantEnd;
                        }

                        if (spriteTimer >= 25)
                        {
                            spellSprite = Resources.Fire_1;
                        }

                        if (spriteTimer >= 30)
                        {
                            spellSprite = Resources.Fire_2;
                        }

                        if (spriteTimer >= 35)
                        {
                            spellSprite = Resources.Fire_3;
                        }

                        if (spriteTimer >= 40)
                        {
                            spellSprite = Resources.Fire_4;
                        }

                        if (spriteTimer >= 45)
                        {
                            spellSprite = Resources.Fire_5;
                        }

                        if (spriteTimer >= 50)
                        {
                            spellSprite = Resources.Fire_6;
                        }

                        if (spriteTimer >= 55)
                        {
                            spellSprite = null;

                            if (spriteTimer > 15 && blackMage.X < 530)
                            {
                                blackMageSprite = Resources.BM_Walk;
                                blackMage.X += 10;
                            }
                            else if (blackMage.X >= 530)
                            {
                                blackMage.X = 530;
                                blackMageSprite = Resources.BM_Idle;
                                animation = "";
                                turn = "noOne";
                            }
                        }

                        break;


                }

            }

            Refresh();
        }
    }
}
