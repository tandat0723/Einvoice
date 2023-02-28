var bShowConfig = false;

function showchangeConfig()
{
    
    if(bShowConfig==false)
    {
        $('#change-config').show();
        bShowConfig = true;
    }
    else
    {
        $('#change-config').hide();
        bShowConfig = false;
    }
}
function ChooseOneDate() {   

    $("#dCalendar").datepicker({
        onSelect: function (dateText, inst) {
            alert(inst);
        }
    });
}
function ChooseDate(from,to)
{
    if (from == 'CUSTOM' || to == 'CUSTOM')
    {
        
        var options = {};
        $('#dCalendar').daterangepicker(options, function (start, end, label) {
            $('#dFromDate').val(start.format('MM/DD/YYYY'));
            $('#dToDate').val(end.format('MM/DD/YYYY'));
            $('#btnSearch').click();
           // console.log('New date range selected: ' + start.format('YYYY-MM-DD') + ' to ' + end.format('YYYY-MM-DD') + ' (predefined range: ' + label + ')');
        }).click();
          
    
    }
    else
    {
        $('#dFromDate').val(from);
        $('#dToDate').val(to);
        $('#btnSearch').click();
    }
}
function validateEmail(email) {
    var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}

function validatePhone(phone) {
    var re = /((09|03|07|08|05)+([0-9]{8})\b)/g;
    return re.test(String(phone));
}

$(document).ready(function () {
    //Load quay theo chi nhanh
    $('#BranchId').change(function () {
        var branchid = $('#BranchId option:selected').val();

        $('#CounterId').empty();
        $('#CounterId').append(new Option("Tất cả quầy", 0));
        if (branchid == '0' || branchid == '')
            return false;

        var param = { branchid: branchid }

        $.post('/Dashboards/GetListCounter', jQuery.param(param))
            .done(function (data) {

                if (data != null) {

                    $.each(data, function (index, itm) {
                        try {

                            $('#CounterId').append(new Option(itm.CounterName, itm.CounterId));

                        } catch (ex) { }
                    })
                }
            })
    })
    //end
})