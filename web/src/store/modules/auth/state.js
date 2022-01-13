import JwtService from '../../../common/JwtService';
import UserService from '../../../common/UserService'

const state = {
    isAuthenticated: JwtService.getToken().token !== '',
    token: JwtService.getToken().token,
    user: UserService.getUser()
}

export default state;