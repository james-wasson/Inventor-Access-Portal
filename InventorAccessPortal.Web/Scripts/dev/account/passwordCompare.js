$().ready(function () {
    'use strict';

    /**
     * Takes
     * @param {String} varifier The selector attached to the verification for the password
     */
    $(".verifyThisPassword").each(function () {
        var p = $(this).first();
        if (p.data("verifier") == null) return;
        if ($(p.data("verifier")).length <= 0) return;
        (function () {
            var $password = p;
            var $verifier = $($password.data("verifier")).first();
            var $disable = $($password.data("verify-disable")).first();
            var $toastError;
            function checkError() {
                if (!equalFunction($password, $verifier)) {
                    $disable.prop('disabled', true);
                    if ($toastError == null) {
                        $toastError = toastr['error']("Passwords do not match.", "", { "closeButton": false});
                    }
                } else {
                    $disable.prop('disabled', false);
                    toastr.clear($toastError);
                    $toastError = null;
                }
            }
            $password.on('input', checkError);
            $verifier.on('input', checkError);
        }());
    });

    function equalFunction($t1, $t2) {
        return $t1.val() === $t2.val();
    }

});
