(function ($) {
    window.blogs = {
        bindBlogDataTable: function () {
            if (!$.fn.DataTable.isDataTable('#blogs')) {
                $('#blogs').dataTable();
            }
            return true;
        },
        unbindBlogDataTable: function () {
            if ($.fn.DataTable.isDataTable('#blogs')) {
                $('#blogs').DataTable().clear().destroy();
            }
            return true;
        }
    }
})(jQuery);
