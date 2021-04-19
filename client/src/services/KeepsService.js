import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'
import { profilesService } from './ProfilesService'

class KeepsService {
  async getAllKeeps() {
    try {
      const res = await api.get('api/keeps')
      console.log(res.data)
      AppState.keeps = res.data
    } catch (error) {
      logger.log(error)
    }
  }

  async deleteKeep(id) {
    try {
      await api.delete('api/keeps/' + id)
      this.getAllKeeps()
      await profilesService.getUserKeeps(AppState.account.id)
    } catch (error) {
      logger.log(error)
    }
  }

  async createKeep(newKeep) {
    try {
      const res = await api.post('api/keeps', newKeep)
      AppState.profileKeeps = [...AppState.profileKeeps, res.data]
    } catch (error) {
      logger.log(error)
    }
  }

  async editKeepCount(id, editKeep) {
    try {
      editKeep.keeps++
      const res = await api.put('api/keeps/' + id, editKeep)
      console.log(res.data)
      AppState.profileKeeps = res.data
    } catch (error) {
      logger.log(error)
    }
  }
}

export const keepsService = new KeepsService()
