const path = require("path");
const webpack = require("webpack");
const ExtractTextPlugin = require("extract-text-webpack-plugin");
const BundleAnalyzerPlugin = require("webpack-bundle-analyzer").BundleAnalyzerPlugin;
const CheckerPlugin = require("awesome-typescript-loader").CheckerPlugin;
const merge = require("webpack-merge");

module.exports = env => {
  const isDevBuild = !(env && env.prod);

  // Configuration in common to both client-side and server-side bundles
  const sharedConfig = () => ({
    stats: { modules: false },
    resolve: {
      extensions: [".js", ".jsx", ".ts", ".tsx"],
      alias: {
        OurComponents: path.resolve(__dirname, "./ClientApp/components/"),
        OurModules: path.resolve(__dirname, "./ClientApp/redux/modules/"),
        OurConfigs: path.resolve(__dirname, "./ClientApp/configs/"),
        OurUtils: path.resolve(__dirname, "./ClientApp/utils/")
      }
    },
    output: {
      filename: "[name].js",
      publicPath: "dist/" // Webpack dev middleware, if enabled, handles requests for this URL prefix
    },
    module: {
      rules: [
        {
          test: /\.tsx?$/,
          include: /ClientApp/,
          use: "awesome-typescript-loader?silent=true"
        },
        { test: /\.(png|jpg|jpeg|gif|svg)$/, use: "url-loader?limit=25000" },
        {
          test: /\.(woff|woff2|eot|ttf)(\?|$)/,
          use: "url-loader?limit=100000"
        }
      ]
    },
    plugins: [
      new CheckerPlugin()
      /*new BundleAnalyzerPlugin({
        analyzerHost: '127.0.0.1',
        analyzerPort: 1234,
      })*/
    ]
  });

  // Configuration for client-side bundle suitable for running in browsers
  const clientBundleOutputDir = "./wwwroot/dist";
  const clientBundleConfig = merge(sharedConfig(), {
    entry: { "main-client": "./ClientApp/boot-client.tsx" },
    module: {
      rules: [
        {
          test: /\.css$/,
          use: ExtractTextPlugin.extract({
            use: isDevBuild ? "css-loader" : "css-loader?minimize"
          })
        },
        {
          test: /\.(scss)$/,
          use: ["css-hot-loader"].concat(
            ExtractTextPlugin.extract({
              use: [
                {
                  loader: "css-loader",
                  options: { alias: { "../img": "../../wwwroot/img" } }
                },
                {
                  loader: "sass-loader"
                }
              ]
            })
          )
        }
      ]
    },
    output: { path: path.join(__dirname, clientBundleOutputDir) },
    plugins: [
      new ExtractTextPlugin("site.css"),
      new webpack.DllReferencePlugin({
        context: __dirname,
        manifest: require("./wwwroot/dist/vendor-manifest.json")
      })
    ].concat(
      isDevBuild
        ? [
            // Plugins that apply in development builds only
            new webpack.SourceMapDevToolPlugin({
              filename: "[file].map", // Remove this line if you prefer inline source maps
              moduleFilenameTemplate: path.relative(
                clientBundleOutputDir,
                "[resourcePath]"
              ) // Point sourcemap entries to the original file locations on disk
            })
          ]
        : [
            // Plugins that apply in production builds only
            new webpack.optimize.UglifyJsPlugin()
          ]
    )
  });

  // Configuration for server-side (prerendering) bundle suitable for running in Node
  const serverBundleConfig = merge(sharedConfig(), {
    resolve: { mainFields: ["main"] },
    entry: { "main-server": "./ClientApp/boot-server.tsx" },
    plugins: [
      new webpack.DllReferencePlugin({
        context: __dirname,
        manifest: require("./ClientApp/dist/vendor-manifest.json"),
        sourceType: "commonjs2",
        name: "./vendor"
      })
    ],
    output: {
      libraryTarget: "commonjs",
      path: path.join(__dirname, "./ClientApp/dist")
    },
    target: "node",
    devtool: "inline-source-map"
  });

  return [clientBundleConfig, serverBundleConfig];
};
