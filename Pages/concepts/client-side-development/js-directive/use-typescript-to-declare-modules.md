# Use TypeScript to declare modules

> The JS directive feature is new in DotVVM 3.0. 

> The JS directive functionality is not supported in Internet Explorer 11. 

If you want to write [JS directive](overview) modules in TypeScript, there are several things to consider. Also, if the JavaScript code base is going to be larger than a single file, it may be useful to use a module bundler.

## Prerequisites

If you don't have the `package.json` file present in the root folder of your app, you can create it by running the following command. It will create the default `package.json` file:

```
npm init -y
```

To install TypeScript and required type definitions, run the following command:

```
npm install --save-dev typescript dotvvm-types @types/knockout
```

> If you want to build TypeScript files as part of the project build using a `Microsoft.TypeScript.MSBuild` NuGet package, refer to the [official documentation](https://www.typescriptlang.org/docs/handbook/integrating-with-build-tools.html#nuget).

## TypeScript configuration

Once the dependencies are installed, you'll need a `tsconfig.json` file. If you don't have such file in the root folder of the app, create it and put the following content inside:

```
{
  "compilerOptions": {
    "target": "es6",
    "moduleResolution": "node",
    "typeRoots": [
      "./node_modules/@types", 
      "./node_modules/dotvvm-types"
    ]
  }
}
```

The output format for the TypeScript compilation must be `es6` or higher, because DotVVM assumes the imported files to be ECMAScript modules. 

In order to see the type hints, you also need to modify the [typeRoots](https://www.typescriptlang.org/tsconfig#typeRoots) configuration so TypeScript will visit also the `dotvvm-types` folder when looking for declaration files.

> See the [TypeScript declarations](../typescript-declarations) page for more info.

## Declare the module

The module can now use TypeScript type annotations as well as TypeScript language features:

```JS
export default (context: DotvvmModuleContext) => new Dashboard(context);

class Dashboard {

    constructor(private context: DotvvmModuleContext) {
        // the "private" keyword will store context in the class instance automatically
        // you don't need to use this.context = context;
    }

}
```

See the [JS directive](overview) chapter for more info about declaring the modules.

## Module dependencies

If your code base involves multiple files, or if you use some libraries, you need to deal with module dependencies.

For example, if you want to use [SignalR](https://docs.microsoft.com/en-us/aspnet/core/signalr/introduction?view=aspnetcore-5.0) in your module, you have two options:

1. You can register a resource for the SignalR library, and specify it as a dependency when registering your module:

```CSHARP
config.Resources.Register("signalr", new ScriptResource()
{
    Location = new UrlResourceLocation("~/lib/signalr/signalr.min.js")
});

config.Resources.Register("dashboard-module", new ScriptModuleResource(new UrlResourceLocation("~/app/dashboard-module.js"))
{
    Dependencies = new [] { "signalr" }
});
```

2. Alternatively, you can bundle the SignalR source code together with your module, and produce a single file that will be loaded in the browser. 

Similarly, if you want to `import` members from different modules, you may want to bundle the files together so the browser won't need to download a lot of small files.

For the first option, you can compile your TypeScript files by running `tsc`, or via the TypeScript MSBuild NuGet package. 

For the second option, you'll need a module bundler. 

## Module bundler

Module bundlers can combine multiple JavaScript modules or libraries into a single file, which usually makes loading the module more efficient. We recommend using [rollup.js](https://rollupjs.org/guide/en/) for this purpose. 

> Webpack is currently not supported right now - it cannot emit ES6 modules which are needed by DotVVM.

### Install rollup

First, you need to install `rollup` and its plugins. Run the following command in the terminal:

```
npm install --save-dev rollup rollup-plugin-commonjs rollup-plugin-node-resolve rollup-plugin-terser @rollup/plugin-typescript
```

### Configure bundling

Now, you can create a configuration file called `rollup.config.js` in the root directory of the app. The file should look like this:

```JS
import typescript from '@rollup/plugin-typescript';
import resolve from 'rollup-plugin-node-resolve';
import commonjs from 'rollup-plugin-commonjs';
import { terser } from 'rollup-plugin-terser';

const minify = process.env.BUILD === 'production';

export default [{
    input: [
        // list all your module files that will be imported in the page
        'wwwroot/app/dashboard.ts'
    ],
    output: {
        dir: 'wwwroot',
        format: 'es',
        sourcemap: !minify
    },
    plugins: [
        typescript(),

        resolve({ browser: true }),
        commonjs(),

        minify && terser({
            ecma: 6,
            compress: true,
            output: {
                beautify: !minify
            }
        })
    ] 
}];
```

Then, open the `package.json` and add the following items in the `scripts` section:

```JS
  ...
  "scripts": {
    "build": "rollup -c",
    "build-production": "rollup -c --environment BUILD:production"
  },
  ...
```

### Create the bundle

To produce a debug version of the bundle, just run `npm run build` in the terminal. 

To build a minified version of the bundle, run `npm run build-production`. You can use this option in your CI/CD pipeline.

## Register the bundle in the app

The generated JavaScript file can now be registered as a `ScriptModuleResource`:

```CSHARP
config.Resources.Register("dashboard-module", new ScriptModuleResource(new UrlResourceLocation("~/app/dashboard-module.js")));
```

## See also

* [JS directive overview](overview)
* [Call JavaScript from DotVVM](call-js-from-dotvvm)
* [Call DotVVM from JavaScript](call-dotvvm-from-js)
* [Use TypeScript to declare modules](use-typescript-to-declare-modules)
* [Sample app: JS directive](https://github.com/riganti/dotvvm-samples-js-integration)


