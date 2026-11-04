// Lab6: AJAX filter, text search and paging. Plain fetch(), no jQuery dependency.
(function () {
    'use strict';

    const app = document.getElementById('lab6App');
    if (!app || app.dataset.ajaxMode !== 'true') return;

    const content = document.getElementById('content');
    const majorList = document.getElementById('majorFilters');
    const form = document.getElementById('searchForm');
    const keywordInput = document.getElementById('keyword');
    const majorInput = document.getElementById('currentMajorId');
    const status = document.getElementById('filterStatus');

    let activeRequest = null;
    let requestNumber = 0;

    function normalizedParams(params) {
        const result = new URLSearchParams();
        const major = params.get('mid');
        const word = (params.get('keyword') || '').trim();
        const page = parseInt(params.get('pageIndex') || '1', 10);

        if (major && /^\d+$/.test(major)) result.set('mid', major);
        if (word) result.set('keyword', word);
        if (Number.isFinite(page) && page > 1) result.set('pageIndex', String(page));
        return result;
    }

    function reflectFilters(params) {
        const major = params.get('mid') || '';
        majorInput.value = major;
        keywordInput.value = params.get('keyword') || '';
        majorList.querySelectorAll('.major-item').forEach(li => {
            li.classList.toggle('active', li.dataset.majorId === major);
        });
    }

    async function refresh(params, addHistory) {
        const query = normalizedParams(params);
        const apiUrl = new URL(app.dataset.filterUrl, window.location.origin);
        apiUrl.search = query.toString();
        const browserUrl = new URL(app.dataset.indexUrl, window.location.origin);
        browserUrl.search = query.toString();

        if (activeRequest) activeRequest.abort();
        const controller = new AbortController();
        activeRequest = controller;
        const thisRequest = ++requestNumber;
        content.setAttribute('aria-busy', 'true');
        status.textContent = 'Đang lọc dữ liệu...';

        try {
            const response = await fetch(apiUrl.toString(), {
                signal: controller.signal,
                headers: { 'X-Requested-With': 'XMLHttpRequest' }
            });
            if (!response.ok) throw new Error('HTTP ' + response.status);
            const markup = await response.text();
            if (thisRequest !== requestNumber) return;

            content.innerHTML = markup;
            reflectFilters(query);
            const result = content.querySelector('.table-result');
            const actualPage = result ? parseInt(result.dataset.page || '1', 10) : 1;
            if (actualPage > 1) query.set('pageIndex', String(actualPage));
            else query.delete('pageIndex');
            browserUrl.search = query.toString();
            if (addHistory) window.history.pushState({ lab6: true }, '', browserUrl.toString());

            const total = result ? result.dataset.total : '?';
            status.textContent = 'Đã tìm thấy ' + total + ' người học. Đang ở trang ' + actualPage + '.';
        } catch (error) {
            if (error.name !== 'AbortError') {
                status.textContent = 'Không tải được danh sách: ' + error.message + '. Hãy thử lại.';
            }
        } finally {
            if (thisRequest === requestNumber) {
                content.removeAttribute('aria-busy');
                activeRequest = null;
            }
        }
    }

    app.addEventListener('click', function (event) {
        const link = event.target.closest('a.major-filter, a.page-link');
        if (!link || !app.contains(link)) return;
        if (event.ctrlKey || event.metaKey || event.altKey || event.shiftKey || event.button !== 0) return;
        event.preventDefault();
        const destination = new URL(link.href, window.location.origin);
        const params = normalizedParams(destination.searchParams);
        // When changing Major, always start from page 1 (avoid empty pages).
        if (link.classList.contains('major-filter')) {
            // Major links are rendered once. Keep the current search field
            // (which may have changed through an earlier AJAX request).
            const enteredKeyword = keywordInput.value.trim();
            if (enteredKeyword) params.set('keyword', enteredKeyword);
            else params.delete('keyword');
            params.delete('pageIndex');
        }
        refresh(params, true);
    });

    form.addEventListener('submit', function (event) {
        event.preventDefault();
        const params = new URLSearchParams();
        if (majorInput.value) params.set('mid', majorInput.value);
        if (keywordInput.value.trim()) params.set('keyword', keywordInput.value.trim());
        // New searches begin at page 1.
        refresh(params, true);
    });

    window.addEventListener('popstate', function () {
        refresh(new URLSearchParams(window.location.search), false);
    });
})();
