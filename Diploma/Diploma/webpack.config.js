"use strict";

var webpack = require('webpack');
var TransferWebpackPlugin = require('transfer-webpack-plugin');

module.exports = {
    entry: {
        app: [
            './node_modules/bootstrap/dist/js/bootstrap.min.js',
            './node_modules/bootstrap/scss/bootstrap.scss',
            './node_modules/angular/angular.min.js',
            './node_modules/angular-animate/angular-animate.min.js',
            './node_modules/angular-route/angular-route.min.js',
            './Styles/Style.less',
            './Scripts/Main.ts'
        ]
    },
    output: {
        path: __dirname + '/Bundles/',
        filename: './app/[name].js'
    },
    resolve: {
        extensions: ['.webpack.js', '.web.js', '.ts', '.js', 'tsx']
    },
    module: {
        rules: [
            {
                test: /\.tsx?$/,
                loader: 'ts-loader',
                exclude: /node_modules/,
                options: {
                    transpileOnly: true
                }
            },
            {
                test: /\.css$/,
                exclude: /node_modules/,
                use: 'style!css!postcss'
            },
            {
                test: /\.js$/,
                exclude: /node_modules/,
                loader: "babel-loader"
            },
            {
                test: /\.html$/,
                exclude: /node_modules/,
                loader: "html-loader?exportAsEs6Default"
            },
            {
                test: /\.less$/,
                exclude: /node_modules/,
                use: [{
                    loader: "style-loader"
                }, {
                    loader: "css-loader",
                    options: { url: false }
                }, {
                    loader: "less-loader"
                }
                ]
            },
            {
                test: /\.scss$/,
                use: [{
                    loader: "style-loader"
                }, {
                    loader: "css-loader"
                }, {
                    loader: "sass-loader"
                }]
            }
        ]
    },

    plugins: [
        new TransferWebpackPlugin([
            { from: 'node_modules/jquery', to: 'lib/jquery' },
            { from: 'node_modules/font-awesome', to: 'lib/font-awesome' },
            { from: 'node_modules/tether', to: 'lib/tether' }
        ])
    ]
};