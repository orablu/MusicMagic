using System;
using System.Collections.Generic;

namespace MusicMagic {
    enum NoteType { Undefined, Piano, Guitar, Drum, Vocal };
    interface INoteStream : IEnumerable<INote> {
        NoteType Type { get; set; }
        IEnumerable<INote> Notes { get; set; }
        IList<INoteSource> Sources { get; set; }
        INoteSource GetSource(int pitch);
        IEnumerable<INote> NotesInRange(int start, int end);
        IEnumerable<INote> NotesInRange(TimeSpan start, TimeSpan end);
        IEnumerator<INote> GetEnumerator();
        bool UpdateNote(INote note);
        void Play();
        void Play(int start, int end);
    }
}
