using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aga.Diagrams.Controls;
using System.Windows;

namespace Aga.Diagrams
{
	public class LinkInfo
	{
		public IPort Source { get; set; }
		public IPort Target { get; set; }
        public IPort Control1 { get; set; }
        public IPort Control2 { get; set; }
		public Point? SourcePoint { get; set; }
		public Point? TargetPoint { get; set; }
        public Point? ControlPoint1 { get; set; }
        public Point? ControlPoint2 { get; set; }

		public LinkInfo(ILink link)
		{
			Source = link.Source;
			Target = link.Target;
            Control1 = link.Control1;
            Control2 = link.Control2;
			SourcePoint = link.SourcePoint;
            ControlPoint1 = link.ControlPoint1;
            ControlPoint2 = link.ControlPoint2;
			TargetPoint = link.TargetPoint;
		}

		public void UpdateLink(ILink link)
		{
			link.Source = Source;
			link.Target = Target;
            link.Control1 = Control1;
            link.Control2 = Control2;
			link.SourcePoint = SourcePoint;
			link.TargetPoint = TargetPoint;
            link.ControlPoint1 = ControlPoint1;
            link.ControlPoint2 = ControlPoint2;
		}
	}
}
