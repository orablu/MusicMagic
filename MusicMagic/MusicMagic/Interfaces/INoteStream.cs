using System;
using System.Collections.Generic;

namespace MusicMagic {
    enum NoteType { Undefined, Piano, Guitar, Drum, Vocal };
    interface INoteStream : IEnumerable<INote> {
        NoteType Type { get; set; }
        IEnumerable<INote> Notes { get; set; }
        IList<INoteSource> Sources { get; set; }
        bool Playing { get; }
        INoteSource GetSource(int pitch);
        IEnumerable<INote> NotesInRange(int start, int end);
        IEnumerable<INote> NotesInRange(TimeSpan start, TimeSpan end);
        bool UpdateNote(INote note);
        void Play();
        void Play(int start, int end);
        void PlayPitch(int pitch);
        void Stop();
        void serialize();
        void saveStream();
        void loadStream();
    }
}
