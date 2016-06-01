using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace TestApp.Flowchart
{
	class Link: INotifyPropertyChanged
	{
		[Browsable(false)]
		public FlowNode Source { get; private set; }
		[Browsable(false)]
		public PortKinds SourcePort { get; private set; }
		[Browsable(false)]
		public FlowNode Target { get; private set; }
		[Browsable(false)]
		public PortKinds TargetPort { get; private set; }
        [Browsable(false)]

		private string _text;
		public string Text
		{
			get { return _text; }
			set 
			{ 
				_text = value;
				OnPropertyChanged("Text");
			}
		}

        private string _content;
        public string Content
        {
            get { return _content; }
            set
            {
                _content = value;
                OnPropertyChanged("Content");
            }
        }

        public Link(FlowNode source, PortKinds sourcePort, FlowNode target, PortKinds targetPort)
		{
			Source = source;
			SourcePort = sourcePort;
			Target = target;
			TargetPort = targetPort;
		}

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged(string name)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(name));
		}

		#endregion
	}

	enum PortKinds { Top, Bottom, Left, Right }
}
