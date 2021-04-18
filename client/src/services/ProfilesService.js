import { AppState } from '../AppState'
import { api } from './AxiosService'

class ProfilesService {
  async GetUserProfile(id) {
    const res = await api.get('api/profiles/' + id)
    AppState.profile = res.data
  }

  async getUserKeeps(id) {
    const res = await api.get('api/profiles/' + id + '/keeps')
    AppState.profileKeeps = res.data
  }

  async getUserVaults(id) {
    const res = await api.get(`api/profiles/${id}/vaults`)
    AppState.profileVaults = res.data
    AppState.isLoading = false
  }
}

export const profilesService = new ProfilesService()
