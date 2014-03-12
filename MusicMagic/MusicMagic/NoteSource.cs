using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.XAudio2;
using System.IO;
using Windows.Storage;
using SharpDX.Multimedia;

namespace MusicMagic {
    class NoteSource : INoteSource {
        private Stream dataStream;

        /// <summary>
        /// Creates a new NoteSource.
        /// </summary>
        public NoteSource() {
            dataStream = null;
        }

        public XAudio2 Device { get; set; }

        private string _path;
        /// <summary>
        /// The path to the note.
        /// </summary>
        public string Path {
            get {
                return _path;
            }
            set {
                _path = value;
                clearDataStream();
            }
        }

        private WaveFormat _format;
        /// <summary>
        /// The format for the source.
        /// </summary>
        public WaveFormat Format {
            get {
                return _format;
            }
            private set {
                _format = value;
                clearSourceVoice();
            }
        }

        private SourceVoice _voice;
        /// <summary>
        /// The SourceVoice for the source.
        /// </summary>
        public SourceVoice Voice {
            get {
                if (_voice == null) {
                    setSourceVoice();
                }
                return _voice;
            }
            private set {
                _voice = value;
            }
        }

        public uint[] PacketsInfo {
            get;
            private set;
        }

        private int _loopBegin;
        /// <summary>
        /// The start of the note's loop.
        /// </summary>
        public int LoopBegin {
            get {
                return _loopBegin;
            }
            set {
                _loopBegin = value;
                clearDataStream();
            }
        }

        private int _loopLength;
        /// <summary>
        /// The length of the note's loop.
        /// </summary>
        public int LoopLength {
            get {
                return _loopLength;
            }
            set {
                _loopLength = value;
                clearDataStream();
            }
        }

        private int _noteLength;
        /// <summary>
        /// The length of the note's source.
        /// </summary>
        public int NoteLength {
            get {
                return _noteLength;
            }
            set {
                _noteLength = value;
                setDataStream();
            }
        }

        /// <summary>
        /// Gets the audio buffer that will play for the given length.
        /// </summary>
        /// <param name="length">The length of the note to play.</param>
        /// <returns>An AudioBuffer that will play for the desired length.</returns>
        public AudioBuffer GetAudioBuffer(int length) {
            if (dataStream == null) {
                setDataStream();
            }
            return new AudioBuffer {
                AudioBytes = (int)dataStream.Length,
                Flags = BufferFlags.EndOfStream,
                LoopBegin = LoopBegin,
                LoopLength = LoopLength,
                LoopCount = getLoopCount(length),
                PlayBegin = 0,
                PlayLength = Math.Min(length, NoteLength)
            };
        }

        /// <summary>
        /// Gets the number of loops required to play a note of the given length.
        /// </summary>
        /// <param name="length">The length of the note.</param>
        /// <returns>The number of loops to play to achieve the given length.</returns>
        private int getLoopCount(int length) {
            var duration = length + LoopLength - NoteLength;
            var ratio = duration / LoopLength;
            return ratio > 0 ? ratio : 0;
        }

        private void clearSourceVoice() {
            if (Voice == null) {
                return;
            }
            Voice.Dispose();
            Voice = null;
        }

        private void clearDataStream() {
            if (dataStream == null) {
                return;
            }
            dataStream.Dispose();
            dataStream = null;
            Format = null;
            PacketsInfo = null;
        }

        /// <summary>
        /// Sets the source voice.
        /// </summary>
        private void setSourceVoice() {
            if (Device == null) {
                throw new NullReferenceException("The XAudio2 device was not specified.");
            }
            if (Format == null) {
                setDataStream();
            }
            Voice = new SourceVoice(Device, Format, true);
        }

        /// <summary>
        /// Sets the data stream using the note's properties.
        /// </summary>
        private void setDataStream() {
            if (Path == null) {
                throw new NullReferenceException("There is no path specified.");
            }
            var localStorage = ApplicationData.Current.LocalFolder;
            var file = localStorage.OpenStreamForReadAsync(Path).Result;
            var stream = new SoundStream(file);
            dataStream = stream.ToDataStream();
            Format = stream.Format;
            PacketsInfo = stream.DecodedPacketsInfo;
            stream.Dispose();
        }

        public void Dispose() {
            clearSourceVoice();
            clearDataStream();
        }
    }
}
