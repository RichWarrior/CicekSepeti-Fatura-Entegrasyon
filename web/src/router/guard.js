var allowScopes = [
    "/auth/login"
]

import store from '../store'
import httpClient from '../common/HttpClient'
import JWTService from '../common/JwtService'

import { DESTROY_USER } from '../store/modules/auth/actions.type'

export const Guard = (router) => {
    router.beforeEach((to, from, next) => {
        var token = JWTService.getToken();
        if (token.token !== undefined || token.token !== '') {
            httpClient.setHeader();
        }

        var isAuthenticated = store.getters.isAuthenticated;
        var relativePath = to.path;

        if (!isAuthenticated) {
            if (!allowScopes.includes(relativePath)) {
                router.push({
                    path: '/auth/login'
                })
            }
        } else {
            if (new Date(token.expireDate) > new Date()) {
                if (allowScopes.includes(relativePath)) {
                    router.push({
                        path: '/files'
                    })
                }
            } else {
                store.dispatch(DESTROY_USER)
                router.push({
                    path: '/auth/login'
                })
            }
        }

        if (to.fullPath == '/') {
            if (isAuthenticated)
                router.push({ path: '/files' })
            else
                router.push({ path: '/auth/login' })
        }
        next();
    })
}