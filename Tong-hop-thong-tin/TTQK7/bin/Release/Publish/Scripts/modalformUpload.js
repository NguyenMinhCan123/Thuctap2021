    $.ajaxSetup({ cache: false });

    $("a[data-modal-upload]").on("click", function (e) {

        // hide dropdown if any
        $(e.target).closest('.btn-group').children('.dropdown-toggle').dropdown('toggle');

        $('#myModalContent').load(this.href, function () {            
            $(":input").inputmask();
            // this is the first add
            $.validator.unobtrusive.parse($('form'));
            //định dạng cho kiểu nhập ngày tháng cho tất cả các kiểu ngày
            //$(".datefield").datepicker({ dateFormat: 'dd/mm/yy', changeYear: true, yearRange: '1950:2030' }, $.datepicker.regional["vi"]);

            $('#myModal').modal({
                /*backdrop: 'static',*/
                keyboard: true
            }, 'show');

            bindFormUpload(this);
        });

        return false;
    });   
function bindFormUpload(dialog) {
    $('form[name="postTaiLieu"]').submit(function (e) {

        var form = $(this);
        var formdata = false;
        if (window.FormData) {
            formdata = new FormData(form[0]);
        }

        var formAction = form.attr('action');
        $.ajax({
            type: this.method,
            url: this.action,
            cache: false,
            data: formdata ? formdata : form.serialize(),
            contentType: false,
            processData: false,

            success: function (result) {
                if (result.success) {
                    $('#myModal').modal('hide');
                    location.reload();
                } else {
                    $('#myModalContent').html(result);
                    bindForm();
                }
            }
        });
        e.preventDefault();
    });
}

