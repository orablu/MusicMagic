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

        private INoteStream _stream;
        public INoteStream Stream {
            get {
                return _stream;
            }
            set {
                _stream = value;
                if (Pitch >= 0) {
                    setNoteSource();
                }
            }
        }

        public XAudio2 Device { get; set; }

        public long Start { get; set; }

        public int Length { get; set; }

        private int _pitch;
        public int Pitch {
            get {
                return _pitch;
            }
            set {
                _pitch = value;
                if (Stream != null) {
                    setNoteSource();
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
            if (Stream == null) {
                throw new NullReferenceException("Parent stream not set.");
            }
            if (Pitch < 0 || Pitch >= Stream.Sources.Count) {
                throw new IndexOutOfRangeException("Pitch is out of range of sources.");
            }
            source = Stream.Sources[Pitch];
        }
    }
}
