using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicMagic {
    class NoteStream : INoteStream {
        private Dictionary<int, SortedSet<INote>> notesInPitch;
        public NoteStream() {
        }

        public NoteType Type { get; set; }

        public IEnumerable<INote> Notes {
            get {
                var notes = new HashSet<INote>();
                foreach (var pitch in notesInPitch) {
                    notes.UnionWith(pitch.Value);
                }
                return notes;
            }
            set {
                notesInPitch = new Dictionary<int, SortedSet<INote>>();
                foreach (var note in value) {
                    Add(note);
                }
            }
        }

        public IList<INoteSource> Sources { get; set; }

        public INoteSource GetSource(int pitch) {
            return Sources[pitch];
        }

        public IEnumerable<INote> NotesInRange(int start, int end) {
            var notes = new HashSet<INote>();
            foreach (var pitch in notesInPitch) {
                notes.UnionWith(from note in pitch.Value
                                where note.Start >= start &&
                                    note.Start + note.Length < end
                                select note);
            }
            return notes;
        }

        public IEnumerator<INote> GetEnumerator() {
            return Notes.GetEnumerator();
        }

        public bool UpdateNote(INote note) {
            // Have to search for note since the pitch may have changes, and cannot be properly indexed.
            var result = false;
            foreach (var pitch in notesInPitch) {
                result |= pitch.Value.Remove(note);
            }
            return result && Add(note);
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return Notes.GetEnumerator();
        }

        private bool Add(INote note) {
            if (!notesInPitch.ContainsKey(note.Pitch)) {
                notesInPitch[note.Pitch] = new SortedSet<INote>();
            }
            return notesInPitch[note.Pitch].Add(note);
        }


        public IEnumerable<INote> NotesInRange(TimeSpan start, TimeSpan end) {
            return NotesInRange((int)start.TotalMilliseconds, (int)end.TotalMilliseconds);
        }

        public void Play() {
            throw new NotImplementedException();
        }

        public void Play(int start, int end) {
            throw new NotImplementedException();
        }
    }
}
