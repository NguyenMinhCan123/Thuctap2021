$(function () {

    $.ajaxSetup({ cache: false });

    $("a[data-modal]").on("click", function (e) {

        // hide dropdown if any
        $(e.target).closest('.btn-group').children('.dropdown-toggle').dropdown('toggle');

        $('#myModalContent').load(this.href, function () {            
            $(":input").inputmask();
            // this is the first add
            $.validator.unobtrusive.parse($('form'));
            //định dạng cho kiểu nhập ngày tháng cho tất cả các kiểu ngày
            $(".datefield").datepicker({ dateFormat: 'dd/mm/yy', changeYear: true, yearRange: '1950:2030' }, $.datepicker.regional["vi"]);

            //đoạn này thực hiện kiểm tra password
            $('#password, #confirm_password').on('keyup', function () {
              if ($('#password').val() == $('#confirm_password').val()) {                
                  $('#messageConfirmPass').html('');
              } else {
                  $('#messageConfirmPass').html('Chưa khớp').css('color', 'red');
              }
            });

            $('#myModal').modal({
                /*backdrop: 'static',*/
                keyboard: true
            }, 'show');

            bindForm(this);
        });

        return false;
    });


});
function bindForm(dialog) {
    $('form', dialog).submit(function () {
        // this is the second addition
        if ($(this).valid()) {
            $.ajax({
                url: this.action,
                type: this.method,
                data: $(this).serialize(),
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
            return false;
        }
    });
}