import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class ProfilesService {
  async GetUserProfile(id) {
    try {
      const res = await api.get('api/profiles/' + id)
      AppState.profile = res.data
    } catch (error) {
      logger.log(error)
    }
  }

  async getUserKeeps(id) {
    try {
      const res = await api.get('api/profiles/' + id + '/keeps')
      AppState.profileKeeps = res.data
    } catch (error) {
      logger.log(error)
    }
  }

  async getUserVaults(id) {
    try {
      const res = await api.get(`api/profiles/${id}/vaults`)
      AppState.profileVaults = res.data
      AppState.isLoading = false
    } catch (error) {
      logger.log(error)
    }
  }
}

export const profilesService = new ProfilesService()
