import { SET_USER, PURGE_USER } from './mutations.type'

import JWTService from '../../../common/JwtService'
import UserService from '../../../common/UserService'

import UserItem from '../../../dto/UserItem'
import TokenItem from '../../../dto/TokenItem'

const mutations = {
    [SET_USER](state, payload) {
        var userItem = new UserItem();
        var tokenItem = new TokenItem();

        userItem.Id = payload.id;
        userItem.Email = payload.email;

        tokenItem.token = payload.token;
        tokenItem.expireDate = payload.expireDate;

        state.user = userItem;
        state.token = tokenItem;

        JWTService.saveToken(tokenItem);
        UserService.saveUser(userItem);
    },
    [PURGE_USER]() {
        JWTService.destroyToken();
        UserService.destroyUser();
    }
}

export default mutations;