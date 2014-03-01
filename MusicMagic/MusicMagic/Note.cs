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
        public List<INoteSource> Sources {
            get;
            set;
        }

        public XAudio2 Device { get; set; }

        public long Start {
            get;
            set;
        }

        private int _length;
        public int Length {
            get {
                return _length;
            }
            set {
                _length = value;
                destroySourceVoice();
            }
        }

        private int _pitch;
        public int Pitch {
            get {
                return _pitch;
            }
            set {
                _pitch = value;
                destroySourceVoice();
            }
        }


        private SourceVoice _sourceVoice;
        private AudioBuffer _buffer;
        public SourceVoice SourceVoice {
            get {
                if (_sourceVoice == null) {
                    _sourceVoice = CreateSourceVoiceFromSource(Sources[Pitch]);
                }
                return _sourceVoice;
            }
        }

        private SourceVoice CreateSourceVoiceFromSource(INoteSource source) {
            var localStorage = ApplicationData.Current.LocalFolder;
            var file = localStorage.OpenStreamForReadAsync(source.Path).Result;
            var stream = new SoundStream(file);
            var format = stream.Format;
            _buffer = new AudioBuffer {
                Stream = stream.ToDataStream(),
                AudioBytes = (int)stream.Length,
                Flags = BufferFlags.EndOfStream,
                LoopBegin = source.LoopBegin,
                LoopLength = source.LoopLength,
                LoopCount = Length
            };
            stream.Dispose();
            return new SourceVoice(Device, format, true);
        }

        private void destroySourceVoice() {
            if (_sourceVoice != null) {
                _sourceVoice.DestroyVoice();
                _buffer.Stream.Dispose();
                _sourceVoice.Dispose();
            }
            _sourceVoice = null;
        }
    }
}
