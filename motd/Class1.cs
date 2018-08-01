using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InfinityScript;

namespace motd
{
    public class motd : BaseScript
    {

        public motd() : base()
        {
            base.Call("setdvar", new Parameter[]
			{
				"motd",
				"Wilkommen auf unseren Server da wir das LSD Script gleakt haben müssen wir natürlich alles erstmal zum laufen bekommen. Für Fehler entschuldigen wir uns! BAN SYSTEN = NOTHING"
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

        }

    }
}
