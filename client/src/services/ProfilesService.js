import { AppState } from '../AppState'
import { api } from './AxiosService'

class ProfilesService {
  async GetUserProfile(id) {
    const res = await api.get('api/profiles/' + id)
    AppState.profile = res.data
  }

  async getUserKeeps(id) {
    const res = await api.get('api/profiles/' + id + '/keeps')
    console.log(res.data)
    AppState.profileKeeps = res.data
  }
}

export const profilesService = new ProfilesService()
