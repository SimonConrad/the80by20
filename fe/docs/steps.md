# Steps

## setting up dev environment

### basics

https://angular.io/guide/setup-local

check node, npm, angular cli versions

`ng version`
`node --version`
`npm --version`

install angular cli

`npm install -g @angular/cli`

create workspace  and initial starer app

`ng new the80by20`

- with routing
- chosen scss https://sass-lang.com/documentation/syntax#scss

install packages defined in package.json

`npm install`

run the application

`npm run ng serve --open`

build application, build:prod is alias to npm cli command in package.json file scripts node, or use default command `npm run build` build details are in angular.json file, like buuild catalog environment.json details etc.

`npm run "build:prod"` 

### eslint
- https://thesoreon.com/blog/how-to-set-up-eslint-with-typescript-in-vs-code
- https://typescript-eslint.io/docs/

## create layout with css grid and flex

- flex https://tobiasahlin.com/blog/common-flexbox-patterns/
- grid https://www.positronx.io/css-grid-layout-holy-grail-angular-ui-tutorial/

## routing with lazy-loaded modules

https://angular.io/guide/lazy-loading-ngmodules

`ng generate module solution-to-problem --route solution-to-problem --module app.module`

## rxjs

-  course RxJS in Angular: Reactive Development
- https://github.com/DeborahK/Angular-RxJS

## walking skeleton

- crud: grid fetching data from in memory source, reactive forms
- rxjs
- tests
- passing data between components -input / output, service + subject
- web-api-client-service
- angular material https://material.angular.io/components/form-field/examples https://tudip.com/blog-post/how-to-install-angular-material/
- input validation https://material.angular.io/components/form-field/examples#form-field-error


installing npm package `npm i angular-in-memory-web-api`

generate fake data from ts interface https://ts-faker.vercel.app/

C# to typescript converter

## security
- authentication mechanism
- roles handling
- https://appdividend.com/2022/02/02/angular-authentication/

## ci / cd

- yaml
- deploy to azure

## install and use angular material controls

## implement use cases


# docummentation

cli

https://angular.io/cli

workspace and project file structure 

https://angular.io/guide/file-structure


