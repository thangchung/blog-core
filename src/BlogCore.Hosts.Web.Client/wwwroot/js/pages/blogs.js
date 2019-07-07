(function () {
    window.blogs = {
        bindBlogDataTable: function () {
            if (!$.fn.DataTable.isDataTable('#blogs')) {
                $('#blogs').DataTable({
                    "paging": true,
                    "lengthChange": false,
                    "searching": true,
                    "ordering": true,
                    "info": true,
                    "autoWidth": true,
                });
            }
            return true;
        }
    }
})();
