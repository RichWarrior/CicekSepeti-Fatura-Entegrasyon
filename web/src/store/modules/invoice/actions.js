import httpClient from '../../../common/HttpClient'
import { GET_INVOICES, PROCESS_INVOCES } from './actions.type'
import { SET_INVOICES } from './mutations.type'

const actions = {
    [GET_INVOICES](context, payload) {
        return new Promise((resolve, reject) => {
            httpClient.get(`/invoice/list/${payload.Id}`).then((payload) => {
                context.commit(SET_INVOICES, payload.data);
                resolve(payload)
            }).catch((err) => {
                reject(err);
            })
        })
    },
    [PROCESS_INVOCES](context, payload) {
        return new Promise((resolve, reject) => {
            httpClient.post('/invoice/processInvoice', payload).then((payload) => {
                resolve(payload)
            }).catch((err) => {
                reject(err);
            })
        })
    }
}

export default actions;