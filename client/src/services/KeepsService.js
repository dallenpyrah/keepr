import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class KeepsService {
  async getAllKeeps() {
    try {
      const res = await api.get('api/keeps')
      AppState.keeps = res.data
    } catch (error) {
      logger.log(error)
    }
  }

  async deleteKeep(id) {
    await api.delete('api/keeps/' + id)
    this.getAllKeeps()
  }

  async createKeep(newKeep) {
    const res = await api.post('api/keeps', newKeep)
    AppState.profileKeeps = [...AppState.profileKeeps, res.data]
  }

  async editKeepCount(id, editKeep) {
    editKeep.keeps++
    const res = await api.put('api/keeps/' + id, editKeep)
    console.log(res.data)
    AppState.profileKeeps = res.data
  }
}

export const keepsService = new KeepsService()
