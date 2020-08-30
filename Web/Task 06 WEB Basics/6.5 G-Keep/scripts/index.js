let storage = new Service();

let trashIcon = "<img src='Resources/Trash.png'/>";

const activeModalNoteClassName = "active-modal-note";
const modalNoteClassName = "modal-note";

const noteContainerClassName = ".note-container";

const noteClassName = "note";
const noteId = "note-id";

let modalNote = document.getElementById("modal-note-id");
let modalNoteId = document.getElementById("hidden-id");
let modalNoteHead = document.getElementById("note-head-id");
let modalNoteBody = document.getElementById("note-body-id");

Run();

function Run() {
    document.querySelector(".add-note-button").onclick = () => onAddNoteButtonClick();
    document.querySelector(".save-note-button").onclick = () => onSaveNoteButtonClick();
    document.querySelector(".close-note-button").onclick = () => onCloseNoteButtonClick();
}

function onAddNoteButtonClick() {
    modalNote.classList.add(activeModalNoteClassName);
    modalNote.classList.remove(modalNoteClassName);
}

function onSaveNoteButtonClick() {
    let note = { head: modalNoteHead.value, body: modalNoteBody.value, id: modalNoteId.value };

    note.id == "" ? note.id = storage.add(note) : updateExistingNote(note);

    closeModalNote();
    updateNotesAfterAction(note);
}

function onCloseNoteButtonClick() {
    closeModalNote();
}

function onNoteContainerClick(note) {
    editNote(note);
}

function onNoteTrashButtonClick(note) {
    removeNote(note);
}

function updateExistingNote(note) {
    let arrayMemoID = document.getElementsByClassName(noteId);

    for (let i = 0; i < arrayMemoID.length; i++) {
        if (arrayMemoID[i].value == note.id) {
            arrayMemoID[i].parentNode.remove();
            break;
        }
    }

    storage.updateById(modalNoteId.value, note);
}

function changeModalNoteClassName(fromName, toName) {
    modalNote.classList.remove(fromName);
    modalNote.classList.add(toName);
}

function clearModalNote() {
    modalNoteId.value = "";
    modalNoteHead.value = "";
    modalNoteBody.value = "";
}

function closeModalNote() {
    clearModalNote();

    changeModalNoteClassName(activeModalNoteClassName, modalNoteClassName);
}

function editNote(note) {
    modalNoteId.value = note.firstChild.value;
    modalNoteHead.value = note.querySelector(".note-head").innerHTML;
    modalNoteBody.value = note.querySelector(".note-body").innerHTML;

    changeModalNoteClassName(modalNoteClassName, activeModalNoteClassName);
}

function removeNote(note) {
    event.stopPropagation();

    if (confirm("Заметка будет удалена.")) {
        let id = note.getElementsByClassName(noteId)[0].value;
        storage.deleteById(id);
        note.remove();
    }
}

function updateNotesAfterAction(changedNote) {
    let noteContainer = document.querySelector(noteContainerClassName);

    let div = CreateDivForNote(changedNote)

    noteContainer.prepend(div);
}

function CreateDivForNote(note) {
    let div = document.createElement("div");

    div.classList.add(noteClassName);

    let id = document.createElement("input");
    id.classList.add(noteId);
    id.setAttribute("type", "hidden");
    id.setAttribute("value", note.id);

    let head = document.createElement("div");
    head.classList.add("note-head");
    head.innerHTML = note.head;

    let body = document.createElement("div");
    body.classList.add("note-body");
    body.innerHTML = note.body;

    let trash = document.createElement("div");
    trash.classList.add("trash");
    trash.innerHTML = trashIcon;

    div.append(id, head, body, trash);

    div.onclick = () => onNoteContainerClick(div);
    trash.onclick = () => onNoteTrashButtonClick(div);

    return div;
}