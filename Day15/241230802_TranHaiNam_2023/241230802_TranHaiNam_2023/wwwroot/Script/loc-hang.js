const ketQua = document.getElementById("ket-qua");
const menu = document.querySelector(".menu");

if (ketQua && menu) {
    const loi = document.getElementById("loi-loc");
    let lanBam = 0;

    menu.addEventListener("click", async function (event) {
        const link = event.target.closest("a[data-url]");
        if (!link || event.ctrlKey || event.metaKey || event.shiftKey || event.altKey) return;

        event.preventDefault();
        const lanNay = ++lanBam;
        loi.hidden = true;

        try {
            const response = await fetch(link.dataset.url);
            if (!response.ok) throw new Error();
            const html = await response.text();
            if (lanNay !== lanBam) return;

            ketQua.innerHTML = html;
            const cu = menu.querySelector("li.active");
            if (cu) cu.classList.remove("active");
            link.closest("li").classList.add("active");
        } catch {
            if (lanNay === lanBam) loi.hidden = false;
        }
    });
}
