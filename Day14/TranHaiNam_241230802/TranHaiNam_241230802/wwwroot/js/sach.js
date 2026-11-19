$(function () {
    let request = null;

    $(document).on('click', '.js-filter', function (event) {
        if (!$('#book-content').length) return;

        event.preventDefault();
        const link = $(this);

        if (request) request.abort();
        $('#ajax-error').prop('hidden', true);

        request = $.ajax({
            url: link.attr('data-url'),
            type: 'GET',
            dataType: 'html'
        }).done(function (html) {
            $('#book-content').html(html);
            $('.menu li').removeClass('active');
            link.closest('li').addClass('active');
        }).fail(function (xhr, status) {
            if (status !== 'abort') {
                $('#ajax-error').text('Không tải được sách. Hãy thử lại.')
                    .prop('hidden', false);
            }
        });
    });
});
