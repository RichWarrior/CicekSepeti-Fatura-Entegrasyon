import httpClient from '../../../common/HttpClient'
import { LOGIN, DESTROY_USER } from './actions.type'
import { SET_USER, PURGE_USER } from './mutations.type'

const actions = {
    [LOGIN](context, payload) {
        return new Promise((resolve, reject) => {
            httpClient.post('/auth/login', payload).then((payload) => {
                context.commit(SET_USER, payload.data);
                resolve(payload)
            }).catch((err) => {
                reject(err);
            })
        })
    },
    [DESTROY_USER](context) {
        return new Promise((resolve) => {
            context.commit(PURGE_USER);
            resolve();
        })
    }
}

export default actions;