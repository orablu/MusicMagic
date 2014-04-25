using SharpDX.Multimedia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.XAudio2;

namespace MusicMagic {
    public interface INoteSource : IDisposable {
        XAudio2 Device { get; set; }
        string Path { get; set; }
        WaveFormat Format { get; }
        SourceVoice Voice { get; }
        uint[] PacketsInfo { get; }
        int LoopBegin { get; set; }
        int LoopLength { get; set; }
        int NoteLength { get; set; }
        AudioBuffer GetAudioBuffer(int length);
        void Play();
        void Stop();
    }
}
