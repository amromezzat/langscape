import { FlashCardSet } from "../../../../models/flashCards/flashCardSet";
import { FlashCardSetDto } from "../../../../models/flashCards/flashCardSetDto";
import { FlashCardSetForm } from "../../../../models/flashCards/flashCardSetForm";
import { FlashCardWord } from "../../../../models/flashCards/flashCardWord";
import { apiRequest } from "../../core/apiRequest";

const endPoint: string = `/flashcards/`;
const favoritesEndPoint: string = endPoint + `favorites/`;

export const flashCardApi = {
    getCardSets: (filters?: Map<string, any>) => apiRequest.get<FlashCardSet[]>(endPoint, filters),
    getCardSet: (setId: string) => apiRequest.get<FlashCardSet>(endPoint + setId),
    addFavoriteSet: (setId: string) => apiRequest.post(favoritesEndPoint + setId, {}),
    removeFavoriteSet: (setId: string) => apiRequest.del(favoritesEndPoint + setId),
    updateWord: (setId: string, word: FlashCardWord) => apiRequest.put(endPoint + setId, {'updatedWords': [word]}),
    updateSet: (setDto: FlashCardSetDto) => apiRequest.put(endPoint + `${setDto.id}`, setDto),
    createSet: (set: FlashCardSetForm) => apiRequest.post<FlashCardSet>(endPoint, set),
    deleteSet: (setId: string) => apiRequest.del(endPoint + setId)
}