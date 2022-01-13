import Vue from 'vue'
import VueRouter from 'vue-router'

import auth from './auth'
import files from './files'

import { Guard } from './guard'


Vue.use(VueRouter)

const routes = [
    auth,
    files
]

const router = new VueRouter({
    mode: 'history',
    base: process.env.BASE_URL,
    routes
})

Guard(router)

export default router