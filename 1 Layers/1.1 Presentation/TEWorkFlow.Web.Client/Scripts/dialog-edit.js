function dlg-add(title, dlgid) {
    if (!dlgid) { dlgid = '#dlg'; }
    $(dlgid).dialog('open').dialog('setTitle', title);
    $('#fm').form('clear');
}
function dlg-edit(title, dlgid, dgid) {
    if (!dlgid) { dlgid = '#dlg'; }
    if (!dgid) { dgid = '#dg'; }
    var row = $(dgid).datagrid('getSelected');
    if (row) {
        $(dlgid).dialog('open').dialog('setTitle', title);
        $('#fm').form('load', row);
    }
}
function dlg-remove(url, dgid) {
    if (!dgid) { dgid = '#dg'; }
    var row = $(dgid).datagrid('getSelected');
    if (row) {
        $.messager.confirm('Confirm', 'Are you sure you want to remove?', function (r) {
            if (r) {
                $.post(url, { id: row.Id }, function (result) {
                    if (result.Success) {
                        $(dgid).datagrid('reload');
                    } else {
                        $.messager.alert('Error', result.Message, 'error');
                    }
                }, 'json');
            }
        });
    }
}
function dlg-save(url, dlgid, dgid) {
    iif (!dlgid) { dlgid = '#dlg'; }
    if (!dgid) { dgid = '#dg'; }
    $('#fm').form('submit', {
        url: url,
        onSubmit: function () {
            return $(this).form('validate');
        },
        success: function (result) {
            var result = eval('(' + result + ')');
            if (result.Success) {
                $(dlgid).dialog('close');
                $(dgid).datagrid('reload');
            } else {
                $.messager.alert('Error', result.Message, 'error');
            }
        }
    });
}