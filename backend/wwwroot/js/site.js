// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

(function () {
    var preventDoubleSubmitSelector = 'form[data-prevent-double-submit]';

    function handleSubmit(event) {
        var form = event.currentTarget;
        var submitButton = form.querySelector('button[type="submit"]');

        if (!submitButton) {
            return;
        }

        if (submitButton.dataset.submitted === 'true') {
            event.preventDefault();
            return;
        }

        submitButton.dataset.submitted = 'true';
        submitButton.disabled = true;
    }

    document.addEventListener('DOMContentLoaded', function () {
        var forms = document.querySelectorAll(preventDoubleSubmitSelector);
        forms.forEach(function (form) {
            form.addEventListener('submit', handleSubmit);
        });
    });
})();
