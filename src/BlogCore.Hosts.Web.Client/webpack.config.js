'use strict';

const path = require('path');
const CopyPlugin = require('copy-webpack-plugin');

const ROOT_PATH = path.resolve(__dirname, '.');
const WWWROOT_PATH = ROOT_PATH + '/wwwroot/plugins/';
const NODE_PACKAGES_PATH = ROOT_PATH + '/node_modules/';

module.exports = {
    plugins: [
        new CopyPlugin([
            {
                context: NODE_PACKAGES_PATH + '@fortawesome/fontawesome-free/',
                from: '**/*',
                to: WWWROOT_PATH + '@fortawesome/fontawesome-free/'
            },
            {
                context: NODE_PACKAGES_PATH + '@fullcalendar/bootstrap/',
                from: '**/*',
                to: WWWROOT_PATH + '@fullcalendar/bootstrap/'
            },
            {
                context: NODE_PACKAGES_PATH + '@fullcalendar/core/',
                from: '**/*',
                to: WWWROOT_PATH + '@fullcalendar/core/'
            },
            {
                context: NODE_PACKAGES_PATH + '@fullcalendar/daygrid/',
                from: '**/*',
                to: WWWROOT_PATH + '@fullcalendar/daygrid/'
            },
            {
                context: NODE_PACKAGES_PATH + '@fullcalendar/interaction/',
                from: '**/*',
                to: WWWROOT_PATH + '@fullcalendar/interaction/'
            },
            {
                context: NODE_PACKAGES_PATH + '@fullcalendar/timegrid/',
                from: '**/*',
                to: WWWROOT_PATH + '@fullcalendar/timegrid/'
            },
            {
                context: NODE_PACKAGES_PATH + '@lgaitan/pace-progress/',
                from: '**/*',
                to: WWWROOT_PATH + '@lgaitan/pace-progress/'
            },
            {
                context: NODE_PACKAGES_PATH + 'bootstrap/',
                from: '**/*',
                to: WWWROOT_PATH + 'bootstrap/'
            },
            {
                context: NODE_PACKAGES_PATH + 'bootstrap-colorpicker/',
                from: '**/*',
                to: WWWROOT_PATH + 'bootstrap-colorpicker/'
            },
            {
                context: NODE_PACKAGES_PATH + 'bootstrap-slider/',
                from: '**/*',
                to: WWWROOT_PATH + 'bootstrap-slider/'
            },
            {
                context: NODE_PACKAGES_PATH + 'chart.js/',
                from: '**/*',
                to: WWWROOT_PATH + 'chartjs/'
            },
            {
                context: NODE_PACKAGES_PATH + 'datatables.net/',
                from: '**/*',
                to: WWWROOT_PATH + 'datatables.net/'
            },
            {
                context: NODE_PACKAGES_PATH + 'datatables.net-bs4/',
                from: '**/*',
                to: WWWROOT_PATH + 'datatables.net-bs4/'
            },
            {
                context: NODE_PACKAGES_PATH + 'daterangepicker/',
                from: '**/*',
                to: WWWROOT_PATH + 'daterangepicker/'
            },
            {
                context: NODE_PACKAGES_PATH + 'fastclick/',
                from: '**/*',
                to: WWWROOT_PATH + 'fastclick/'
            },
            {
                context: NODE_PACKAGES_PATH + 'flot/',
                from: '**/*',
                to: WWWROOT_PATH + 'flot/'
            },
            {
                context: NODE_PACKAGES_PATH + 'icheck-bootstrap/',
                from: '**/*',
                to: WWWROOT_PATH + 'icheck-bootstrap/'
            },
            {
                context: NODE_PACKAGES_PATH + 'inputmask/',
                from: '**/*',
                to: WWWROOT_PATH + 'inputmask/'
            },
            {
                context: NODE_PACKAGES_PATH + 'ion-rangeslider/',
                from: '**/*',
                to: WWWROOT_PATH + 'ion-rangeslider/'
            },
            {
                context: NODE_PACKAGES_PATH + 'jquery/',
                from: '**/*',
                to: WWWROOT_PATH + 'jquery/'
            },
            {
                context: NODE_PACKAGES_PATH + 'jquery-knob-chif/',
                from: '**/*',
                to: WWWROOT_PATH + 'jquery-knob-chif/'
            },
            {
                context: NODE_PACKAGES_PATH + 'jquery-mapael/',
                from: '**/*',
                to: WWWROOT_PATH + 'jquery-mapael/'
            },
            {
                context: NODE_PACKAGES_PATH + 'jquery-mousewheel/',
                from: '**/*',
                to: WWWROOT_PATH + 'jquery-mousewheel/'
            },
            {
                context: NODE_PACKAGES_PATH + 'jquery-ui-dist/',
                from: '**/*',
                to: WWWROOT_PATH + 'jquery-ui-dist/'
            },
            {
                context: NODE_PACKAGES_PATH + 'jqvmap/',
                from: '**/*',
                to: WWWROOT_PATH + 'jqvmap/'
            },
            {
                context: NODE_PACKAGES_PATH + 'moment/',
                from: '**/*',
                to: WWWROOT_PATH + 'moment/'
            },
            {
                context: NODE_PACKAGES_PATH + 'overlayscrollbars/',
                from: '**/*',
                to: WWWROOT_PATH + 'overlayscrollbars/'
            },
            {
                context: NODE_PACKAGES_PATH + 'popper.js/',
                from: '**/*',
                to: WWWROOT_PATH + 'popper.js/'
            },
            {
                context: NODE_PACKAGES_PATH + 'raphael/',
                from: '**/*',
                to: WWWROOT_PATH + 'raphael/'
            },
            {
                context: NODE_PACKAGES_PATH + 'select2/',
                from: '**/*',
                to: WWWROOT_PATH + 'select2/'
            },
            {
                context: NODE_PACKAGES_PATH + 'sparklines/',
                from: '**/*',
                to: WWWROOT_PATH + 'sparklines/'
            },
            {
                context: NODE_PACKAGES_PATH + 'summernote/',
                from: '**/*',
                to: WWWROOT_PATH + 'summernote/'
            },
            {
                context: NODE_PACKAGES_PATH + 'sweetalert2/',
                from: '**/*',
                to: WWWROOT_PATH + 'sweetalert2/'
            },
            {
                context: NODE_PACKAGES_PATH + 'tempusdominus-bootstrap-4/',
                from: '**/*',
                to: WWWROOT_PATH + 'tempusdominus-bootstrap-4/'
            },
            {
                context: NODE_PACKAGES_PATH + 'toastr/',
                from: '**/*',
                to: WWWROOT_PATH + 'toastr/'
            },
        ])
    ]
};