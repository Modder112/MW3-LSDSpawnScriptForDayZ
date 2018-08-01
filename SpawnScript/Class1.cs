using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InfinityScript;
namespace SpawnScript
{
    public class SpawnScript : BaseScript
    {
        
        public SpawnScript()
            : base()
        {
            CreateSpawnPoints();
            base.Call("setdvar", new Parameter[]
			{
				"motd",
				"Wilkommen auf den LSD-Servern, Dieses ist einen Map die von LSD gescriptet wurde. Es können noch Bugs exestieren. Bitte meldet diesen den bei LSDSnipeServer@hotmail.com."
			});
            HudElem motd = HudElem.CreateServerFontString("boldFont", 1f);
            motd.SetPoint("CENTER", "BOTTOM", 0, -19);
            motd.Foreground = true;
            motd.HideWhenInMenu = true;
            base.OnInterval(25000, delegate
            {
                motd.SetText(this.Call<string>("getdvar", new Parameter[]
				{
					"motd"
				}));
                motd.SetPoint("CENTER", "BOTTOM", 1100, -10);
                motd.Call("moveovertime", new Parameter[]
				{
					25
				});
                motd.X = -700f;
                return true;
            });


            if (getDvar<string>("mapname").Equals("mp_dome") || getDvar<string>("mapname").Equals("mp_interchange") || getDvar<string>("mapname").Equals("mp_bravo"))
            {
                base.PlayerConnected += PlayerConnect;
                base.PlayerDisconnected += PlayerDisconnect;
            }
        }

        public void CreateSpawnPoints()
        {
            if(getDvar<string>("mapname").Equals("mp_dome"))
            {                
                AddSpawnPoint(new Vector3(-2289.554F, 63.77071F, -419.5307F));
                AddSpawnPoint(new Vector3(-3065.594F, 56.32166F, -416.0569F));
                AddSpawnPoint(new Vector3(-3800.978F, 109.526F, -423.2344F));
                AddSpawnPoint(new Vector3(-3955.76F, -699.6914F, -502.3943F));
                AddSpawnPoint(new Vector3(-3839.826F, -1605.44F, -455.6884F));
                AddSpawnPoint(new Vector3(-3114.076F, -2133.724F, -410.2882F));
                AddSpawnPoint(new Vector3(-2402.914F, -2377.135F, -422.8232F));
            }
            if (getDvar<string>("mapname").Equals("mp_interchange"))
            {
                AddSpawnPoint(new Vector3(-5136.251F, 9455.421F, 512.1249F));
                AddSpawnPoint(new Vector3(-6891.003F, 11315.72F, 512.125F));
                AddSpawnPoint(new Vector3(-3907.517F, 11533.7F, 512.125F));
                AddSpawnPoint(new Vector3(-6071.082F, 13312.13F, 512.125F));
                AddSpawnPoint(new Vector3(-6433.944F, 15621.27F, 512.125F));
                AddSpawnPoint(new Vector3(-4766.018F, 15782.49F, 512.125F));
                AddSpawnPoint(new Vector3(-2307.318F, 16683.29F, 512.125F));
                AddSpawnPoint(new Vector3(-3104.113F, 19497.44F, 512.125F));
                AddSpawnPoint(new Vector3(-3185.167F, 15699.35F, 512.125F));
                AddSpawnPoint(new Vector3(-2578.224F, 14489.43F, 512.125F));
                Call("setmapcenter", new Parameter[] {
                new Vector3(-6433.944F, 15621.27F, 512.125F)
                });
            }
            if (getDvar<string>("mapname").Equals("mp_bravo"))
            {
                AddSpawnPoint(new Vector3(-866.1363F, -2167.978F, 1114.125F));
                AddSpawnPoint(new Vector3(-987.2558F, -3720.41F, 1002.125F));
                AddSpawnPoint(new Vector3(-2526.221F, -3407.454F, 1144.336F));
                AddSpawnPoint(new Vector3(-2013.257F, -1837.197F, 1250.884F));
                AddSpawnPoint(new Vector3(-1405.125F, -943.1257F, 1019.563F));
                AddSpawnPoint(new Vector3(-2071.125F, -693.1249F, 1015.675F));
                AddSpawnPoint(new Vector3(-1935.959F, -401.1251F, 1020.125F));
                AddSpawnPoint(new Vector3(-2564.598F, 952.8747F, 1091.985F));
                AddSpawnPoint(new Vector3(-2566.903F, 1296.309F, 1107.522F));
                AddSpawnPoint(new Vector3(-1926.998F, 1316.102F, 1108.125F));
                AddSpawnPoint(new Vector3(-485.125F, 1515.994F, 1232.696F));
                AddSpawnPoint(new Vector3(-171.125F, 1667.82F, 1189.92F));
                AddSpawnPoint(new Vector3(83.62811F, 1432.038F, 1154.424F));
                AddSpawnPoint(new Vector3(1155.846F, 1173.125F, 1168.027F));
                AddSpawnPoint(new Vector3(1807.875F, 1067.059F, 1207.318F));
                AddSpawnPoint(new Vector3(1693.489F, 405.125F, 1186.401F));
                AddSpawnPoint(new Vector3(2283.445F, 1350.162F, 1262.768F));
                AddSpawnPoint(new Vector3(3245.13F, 266.0371F, 1404.527F));
                AddSpawnPoint(new Vector3(2652.874F, -851.7629F, 1474.125F));
                AddSpawnPoint(new Vector3(2060.329F, -2798.809F, 1183.125F));
                AddSpawnPoint(new Vector3(607.5048F, -3488.372F, 1089.603F));
            }
        }
               

        private T getDvar<T>(string dvar)
        {
            // would switch work here? - no
            if (typeof(T) == typeof(int))
            {
                return Call<T>("getdvarint", dvar);
            }
            else if (typeof(T) == typeof(float))
            {
                return Call<T>("getdvarfloat", dvar);
            }
            else if (typeof(T) == typeof(string))
            {
                return Call<T>("getdvar", dvar);
            }
            else if (typeof(T) == typeof(Vector3))
            {
                return Call<T>("getdvarvector", dvar);
            }
            else
            {
                return default(T);
            }
        }

        public struct StartSpawnPT
        {
            public Vector3 Point;
            public TeamNames Team;
        };

        public struct SpawnPT
        {
            public Vector3 Point;
        };

        public enum TeamNames
        {
            Axis = 0,
            Allies = 1,
            FreeForAll = 2,
            None = 3,
        };

        public static float SpawnRadius = 5000.0F;

        public List<SpawnPT> SpawnPoints = new List<SpawnPT>();
        public List<StartSpawnPT> StartSpawnPoints = new List<StartSpawnPT>();
        public List<Entity> PlayerList = new List<Entity>();

        #region ServerFuncs

        public void PlayerConnect(Entity Player)
        {
            PlayerList.Add(Player);
            Player.SpawnedPlayer += new Action(() =>
            {
                SpawnPlayer(Player);
            });
        }

        public void PlayerDisconnect(Entity Player)
        {
            PlayerList.Remove(Player);
        }

        public void SpawnPlayer(Entity Player)
        {
            SpawnPT SP = FindSpawnPoint(Player);
            Player.Call("setorigin", new Parameter[] { SP.Point });
        }

        #endregion

        #region AddSpawnPoint
        public void AddSpawnPoint(Vector3 Point)
        {
            SpawnPT SpawnPoint = new SpawnPT();
            SpawnPoint.Point = Point;
            SpawnPoints.Add(SpawnPoint);
            
        }

        public void AddSpawnPoint(float x, float y, float z)
        {
            Vector3 Point = new Vector3(x, y, z);
            SpawnPT SpawnPoint = new SpawnPT();
            SpawnPoint.Point = Point;
            SpawnPoints.Add(SpawnPoint);
        }

        public void AddStartSpawnPoint(Vector3 Point, TeamNames Team)
        {
            StartSpawnPT SpawnPoint = new StartSpawnPT();
            SpawnPoint.Point = Point;
            SpawnPoint.Team = Team;
        }

        public void AddStartSpawnPoint(float x, float y, float z, TeamNames Team)
        {
            Vector3 Point = new Vector3(x, y, z);
            StartSpawnPT SpawnPoint = new StartSpawnPT();
            SpawnPoint.Point = Point;
            SpawnPoint.Team = Team;
        }
        #endregion

        public StartSpawnPT FindStartSpawnPoint(TeamNames Team)
        {
            return new StartSpawnPT();
        }
        public StartSpawnPT FindStartSpawnPoint(string Team)
        {
            TeamNames TeamB = GetTeamByTeamString(Team);
            return FindStartSpawnPoint(TeamB);
        }
        public StartSpawnPT FindStartSpawnPoint(Entity player)
        {
            string sessionteam = player.GetField<string>("sessionteam");
            TeamNames Team = GetTeamByTeamString(sessionteam);
            return FindStartSpawnPoint(Team);
        }
        
        public SpawnPT FindSpawnPoint(Entity Player)
        {
            SpawnPT SpawnPoint = SpawnPoints[3];
            bool havefind = false;
            foreach (SpawnPT sp in SpawnPoints)
            {
                foreach (Entity p in PlayerList)
                {                    
                    string sessionteam = Player.GetField<string>("sessionteam");
                    string osessionteam = p.GetField<string>("sessionteam");
                    if (osessionteam.Equals("spectator"))
                    { }
                    else if(sessionteam.Equals("none"))
                    {
                        Log.Write(LogLevel.All, osessionteam);
                            if (PlayerToPoint(SpawnRadius, p, sp.Point) == true)
                            {
                                break;
                            }
                            else
                            {
                                SpawnPoint = sp;
                                havefind = true;
                                break;
                            }
                    }
                    else
                    {
                        if (!(sessionteam.Equals(osessionteam)))
                        {
                            Log.Write(LogLevel.All, osessionteam);
                            if (PlayerToPoint(SpawnRadius, p, sp.Point) == true)
                            {
                                break;
                            }
                            else
                            {
                                SpawnPoint = sp;
                                havefind = true;
                                break;
                            }
                        }
                    }
                }
                if (havefind == true)
                {
                    break;
                }
            }
            return SpawnPoint;
        }

        #region NiceFuncs



        public bool PlayerToPoint(float radius, Entity Player, Vector3 Pos)
        {
            float rage = Call<float>("distance",new Parameter [] { 
                Player.Origin, 
                Pos
            });
            Log.Write(LogLevel.All,Player.Name.ToString() + ": " + rage);
            if (rage < SpawnRadius)
            {
                return true;
            }
            return false;
        }

        public TeamNames GetTeamByTeamString(string Team)
        {
            if (Team.Equals("allies"))
            {
                return TeamNames.Allies;
            }
            else if (Team.Equals("axis"))
            {
                return TeamNames.Axis;
            }
            else
            {
                return TeamNames.None;
            }
        }

        #endregion

    }
}
