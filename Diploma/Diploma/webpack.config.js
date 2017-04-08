"use strict";

var webpack = require('webpack');
var TransferWebpackPlugin = require('transfer-webpack-plugin');

var HtmlWebpackPlugin = require('html-webpack-plugin');

module.exports = {
    entry: {
        bundle: './Scripts/Main.ts',
        vendor: [
            './Bundles/lib/bootstrap/dist/js/bootstrap.min.js',
            './Bundles/lib/angular/angular.min.js',
            './Bundles/lib/angular-animate/angular-animate.min.js',
            './Bundles/lib/angular-route/angular-route.min.js',
            './Styles/Style.less'
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
                    loader: "css-loader"
                }, {
                    loader: "less-loader"
                }
                ]
            },
        ]
    },

    plugins: [
        new TransferWebpackPlugin([
            { from: 'node_modules/angular', to: 'lib/angular' },
            { from: 'node_modules/angular-route', to: 'lib/angular-route' },
            { from: 'node_modules/angular-animate', to: 'lib/angular-animate' },
            { from: 'node_modules/bootstrap', to: 'lib/bootstrap' },
            { from: 'node_modules/jquery', to: 'lib/jquery' },
            { from: 'node_modules/font-awesome', to: 'lib/font-awesome' },
            { from: 'node_modules/tether', to: 'lib/tether' }
        ])
    ]
};

function GetBundles(url) {
    return '/Bundles/' + url;
}