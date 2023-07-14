function confirmDelete(uniqueId, isDeleteClick) {
    var deleteSpan = 'deleteSpan' + uniqueId;
    var confirmDeleteSpan = 'confirmDeleteSpan' + uniqueId;

    if (isDeleteClick) {
        $('#' + deleteSpan).hide();
        $('#' + confirmDeleteSpan).show();
    }
    else {
        $('#' + confirmDeleteSpan).hide();
        $('#' + deleteSpan).show();
    }
}