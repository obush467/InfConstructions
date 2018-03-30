using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Map;
using InfConstractions;
using InfConstractions.Views;

namespace InfConstractions.Modules
{
    public class MapDemoModule : DemoModule
    {
        public MapDemoModule()
        {
        }

        public override void Leave()
        {
            foreach (MapControl map in DemoUtils.FindMap(this))
                map.HideToolTip();
            base.Leave();
        }

        public override bool AllowRtl { get { return false; } }
    }
}
