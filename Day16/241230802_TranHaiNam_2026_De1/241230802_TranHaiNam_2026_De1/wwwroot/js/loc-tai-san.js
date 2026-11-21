$(function () {
    const vung = $("#danh-sach");
    let yeuCau;
    $(".menu").on("click", "a", function (event) {
        if (!vung.length || event.ctrlKey || event.metaKey || event.shiftKey || event.altKey) return;
        event.preventDefault();
        const link = $(this);
        const url = link.attr("href");
        if (yeuCau) yeuCau.abort();

        yeuCau = $.get(url, { chiLayDanhSach: true })
            .done(function (html) {
                vung.html(html);
                $(".menu li").removeClass("active");
                link.parent().addClass("active");
            })
            .fail(function (xhr, status) {
                if (status !== "abort") window.location.assign(url);
            });
    });
});
