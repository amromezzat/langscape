import { FlashCardSetForm } from "./flashCardSetForm";
import { NewFlashCardWord } from "./flashCardWord";

export class FlashCardSetCreateDto {
    id?: string = undefined;
    name: string = '';
    words: NewFlashCardWord[] = [];

    constructor(setForm?: FlashCardSetForm) {
        if(setForm) {
          this.id = setForm.id!;
          this.name = setForm.name;
          this.words = setForm.words.map((word, index) => {
            return {...word, position: index} as NewFlashCardWord;
          });   
        }
    }
}