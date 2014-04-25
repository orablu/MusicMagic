using SharpDX.Multimedia;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.XAudio2;
using Windows.Storage;
using System.ComponentModel;

namespace MusicMagic {
    class Note : INote {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private INoteSource source;

        public Note() {
            source = null;
            _pitch = -1;
        }

        private INoteStream _parent;
        public INoteStream Parent {
            get {
                return _parent;
            }
            set {
                _parent = value;
                if (Pitch >= 0) {
                    setNoteSource();
                    Parent.UpdateNote(this);
                }
                OnPropertyChanged("Parent");
            }
        }

        private XAudio2 _device;
        public XAudio2 Device {
            get { return _device; }
            set {
                _device = value;
                OnPropertyChanged("Device");
            }
        }

        private int _start;
        public int Start {
            get { return _start; }
            set {
                _start = value;
                OnPropertyChanged("Start");
            }
        }

        private int _length;
        public int Length {
            get { return _length; }
            set {
                _length = value;
                OnPropertyChanged("Length");
            }
        }

        private int _pitch;
        public int Pitch {
            get {
                return _pitch;
            }
            set {
                _pitch = value;
                if (Parent != null) {
                    setNoteSource();
                }
                OnPropertyChanged("Pitch");
            }
        }

        public void Play() {
            var voice = source.Voice;
            var buffer = source.GetAudioBuffer(Length);
            voice.SubmitSourceBuffer(
                source.GetAudioBuffer(Length),
                source.PacketsInfo);
            voice.Start();
        }

        private void setNoteSource() {
            if (Parent == null) {
                throw new NullReferenceException("Parent stream not set.");
            }
            if (Pitch < 0 || Pitch >= Parent.Sources.Count) {
                throw new IndexOutOfRangeException("Pitch is out of range of sources.");
            }
            source = Parent.Sources[Pitch];
        }

        public int CompareTo(INote other) {
            return this.Start - other.Start;
        }

        public bool Equals(INote other) {
            return this.Pitch == other.Pitch &&
                this.Start == other.Start &&
                this.Length == other.Length &&
                this.Parent == other.Parent;
        }
    }
}
