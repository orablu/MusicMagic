using SharpDX.Multimedia;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.XAudio2;
using Windows.Storage;

namespace MusicMagic {
    class Note : INote {
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
            }
        }

        public XAudio2 Device { get; set; }

        public int Start { get; set; }

        public int Length { get; set; }

        private int _pitch;
        public int Pitch {
            get {
                return _pitch;
            }
            set {
                _pitch = value;
                if (Parent != null) {
                    setNoteSource();
                    Parent.UpdateNote(this);
                }
            }
        }

        public void Play() {
            var voice = source.Voice;
            var buffer = source.GetAudioBuffer(Length);
            voice.SubmitSourceBuffer(
                source.GetAudioBuffer(Length),
                source.PacketsInfo);
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
    }
}
