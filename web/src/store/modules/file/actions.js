import httpClient from '../../../common/HttpClient'
import { UPLOAD_FILE, GET_FILES, DOWNLOAD_FILE } from './actions.type'
import { SET_FILES } from './mutations.type'

const actions = {
    [UPLOAD_FILE](context, payload) {
        return new Promise((resolve, reject) => {
            httpClient.postFile('/files/upload', payload).then((payload) => {
                resolve(payload)
            }).catch((err) => {
                reject(err)
            })
        })
    },
    [GET_FILES](context) {
        return new Promise((resolve, reject) => {
            httpClient.get('/files/list').then((payload) => {
                context.commit(SET_FILES, payload.data);
                resolve(payload)
            }).catch((err) => {
                reject(err);
            })
        })
    },
    [DOWNLOAD_FILE](context, payload) {
        return new Promise((resolve, reject) => {
            httpClient.get(`/files/download/${payload.id}`).then((payload) => {
                resolve(payload.data)
            }).catch((err) => {
                reject(err);
            })
        })
    }
}

export default actions;