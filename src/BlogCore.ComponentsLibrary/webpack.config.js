'use strict';

const path = require('path');
const CopyPlugin = require('copy-webpack-plugin');

const ROOT_PATH = path.resolve(__dirname, '.');
const WWWROOT_PATH = ROOT_PATH + '/wwwroot/plugins/';
const NODE_PACKAGES_PATH = ROOT_PATH + '/node_modules/';

module.exports = {
    plugins: [
        new CopyPlugin([
            /* oidc-client */
            {
                context: NODE_PACKAGES_PATH + 'oidc-client/',
                from: 'dist/oidc-client.min.js',
                to: WWWROOT_PATH + 'oidc-client/dist/'
            },
            /* fontawesome */
            {
                context: NODE_PACKAGES_PATH + '@fortawesome/fontawesome-free/',
                from: 'css/**/*',
                to: WWWROOT_PATH + 'fontawesome-free/dist/'
            },
            {
                context: NODE_PACKAGES_PATH + '@fortawesome/fontawesome-free/',
                from: 'webfonts/**/*',
                to: WWWROOT_PATH + 'fontawesome-free/dist/'
            },
            /* ionicons */
            {
                context: NODE_PACKAGES_PATH + 'ionicons/',
                from: 'dist/css/*',
                to: WWWROOT_PATH + 'ionicons/'
            },
            {
                context: NODE_PACKAGES_PATH + 'ionicons/',
                from: 'dist/fonts/*',
                to: WWWROOT_PATH + 'ionicons/'
            },
            /* bootstrap */
            {
                context: NODE_PACKAGES_PATH + 'bootstrap/',
                from: 'dist/**/*',
                to: WWWROOT_PATH + 'bootstrap/'
            },
            /* datatables */
            {
                context: NODE_PACKAGES_PATH + 'datatables.net-bs4/',
                from: 'css/dataTables.bootstrap4.min.css',
                to: WWWROOT_PATH + 'datatables/dist/css/'
            },
            {
                context: NODE_PACKAGES_PATH + 'datatables.net-bs4/',
                from: 'js/dataTables.bootstrap4.min.js',
                to: WWWROOT_PATH + 'datatables/dist/js/'
            },
            {
                context: NODE_PACKAGES_PATH + 'datatables.net/',
                from: 'js/jquery.dataTables.min.js',
                to: WWWROOT_PATH + 'datatables/dist/js/'
            },
            /* fastclick */
            {
                context: NODE_PACKAGES_PATH + 'fastclick/',
                from: 'lib/fastclick.js',
                to: WWWROOT_PATH + 'fastclick/dist/'
            },
            /* jquery */
            {
                context: NODE_PACKAGES_PATH + 'jquery/',
                from: 'dist/jquery.min.js',
                to: WWWROOT_PATH + 'jquery/dist/'
            }
        ])
    ]
};