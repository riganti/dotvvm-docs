# TypeScript declarations

If you plan to write a lot of client-side code, [TypeScript](https://www.typescriptlang.org/) can save you a lot of time.

Using TypeScript in DotVVM projects is not different from using TypeScript with other frameworks. The TypeScript declaration files (`.d.ts`) for DotVVM are published on npm.


## Install npm packages

If you don't have the `package.json` file present in the root folder of your app, you can create it by running the following command. It will create the default `package.json` file:

```
npm init -y
```

## Install TypeScript

The most convenient option is to have TypeScript CLI tool installed globally. You can find out by running `tsc --version` in the command-line - if it reports that it doesn't know `tsc`, you don't have it installed.

To install TypeScript globally, run the following command:

```
npm install -g typescript
```

> If you want to build TypeScript files as part of the project build using a `Microsoft.TypeScript.MSBuild` NuGet package, refer to the [official documentation](https://www.typescriptlang.org/docs/handbook/integrating-with-build-tools.html#nuget).

## Get DotVVM type declarations

The main advantage of TypeScript is type checking and IntelliSense. If your code interacts with DotVVM client-side API or with Knockout observables, you'll need to add the declaration files for these libraries.

Run the following command in the console:

```
npm install --save-dev dotvvm-types
```

## tsconfig.json

Once the dependencies are installed, you'll need a `tsconfig.json` file to tell DotVVM about the type declarations. If you don't have such file in the root folder of the app, create it and put the following content inside:

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

If you already have the `tsconfig.json` file present in the project, make sure to copy the [typeRoots](https://www.typescriptlang.org/tsconfig#typeRoots) section so it is the same as in the sample above.

Now, the IntelliSense should work for the `dotvvm` global object.

## See also

* [Client-side development overview](overview)
* [JS directive](js-directive/overview)
* [Access validation errors from JS](access-validation-errors-from-js)
