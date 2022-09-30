# Steps

## setting up dev environment

### basics

https://angular.io/guide/setup-local

check node, npm, angular cli versions

`ng version`

install angular cli

`npm install -g @angular/cli`

create workspace  and initial starer app

`ng new the80by20`

- with routing
- chosen scss https://sass-lang.com/documentation/syntax#scss

run the application

`ng serve --open`

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



# todo

 
 - copy solution from repos: deborath, angular university
 - busy indicator generic way
 - translations
 - generic way of showing be error - interceptors
 - apply ci / cd 
 - layout with css grid and flex
 - authentication with jwt, interceptors, like in angular university github
 - angular material
 - tests
 - rxjs
 - code the80by20 use cases - start with these with already done be
 - style cop, formatter, tslint?
 - usefull vs code plugins for angular, scss and html
 - developer.mozilla.org
 - angular.io
