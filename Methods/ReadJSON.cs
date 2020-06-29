using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;

namespace TakeHomeTestCheetah.Methods
{
    class ReadJSON : Recepient
    {
        public string FilePath { get; set; }
        public string RecepientsSelected { get { return recepientsSelected; } }

        private string _jsonText;
        private Recepient.RecipientsItemList RecipientItemList = new Recepient.RecipientsItemList(); 
       
        private string _itemListName1;
        private string _itemListName2;
        private Recepient.Recipients _recepient1 = new Recepient.Recipients();
        private Recepient.Recipients _recepient2 = new Recepient.Recipients();

        private string recepientsSelected = string.Empty;
        private bool promoInd = false; //Check for (promo may only appear once in a list)

        public void ReadJsonFile()
        {
            _jsonText = File.ReadAllText(FilePath);
            RecipientItemList = JsonConvert.DeserializeObject<Recepient.RecipientsItemList>(_jsonText); 

            for (int i = 0; i < RecipientItemList.recipients.Count; i++)
            {
                _recepient1 = RecipientItemList.recipients[i];
                _itemListName1 = _recepient1.name;

                for (int j = 0; j < RecipientItemList.recipients.Count; j++)
                {
                    _recepient2 = RecipientItemList.recipients[j];
                    _itemListName2 = _recepient2.name;

                    if (_itemListName1 != _itemListName2 && !recepientsSelected.Contains(_itemListName1)) //Each pair of names should only appear once in the list, the order does not matter.
                    {
                        if (promoInd == true) //Check for (promo may only appear once in a list)
                        {
                            if (GetIndexOfItemTags(_recepient1,"promo") > -1)
                                RemoveItemTagsByIndex(_recepient1,GetIndexOfItemTags(_recepient1,"promo"));
                            if (GetIndexOfItemTags(_recepient2,"promo") > -1)
                                RemoveItemTagsByIndex(_recepient2,GetIndexOfItemTags(_recepient2,"promo"));
                        } 
                        if (_recepient1.tags.Intersect(_recepient2.tags).Count() >= 2) //a list of each pair of names which have 2 or more tags in common
                        {
                            if (GetIndexOfItemTags(_recepient1,"promo") > -1 || GetIndexOfItemTags(_recepient2,"promo") > -1)
                                promoInd = true;
                            ConCatRecepientsSelected(_itemListName1, _itemListName2);
                            break;
                        }
                    }
                }
            }
        }

        private void ConCatRecepientsSelected(string recepientName1, string recepientName2)
        {
            if (recepientsSelected == string.Empty) //in the format "name1, name2|name1, name2|..."
                recepientsSelected = recepientName1 + ", " + recepientName2;
            else
                recepientsSelected = recepientsSelected + "|" + recepientName1 + ", " + recepientName2;
        }

        private int GetIndexOfItemTags(Recepient.Recipients recepientItem ,string Item)
        {
            return recepientItem.tags.IndexOf(Item);
        }

        private void RemoveItemTagsByIndex(Recepient.Recipients recepientItem, int index)
        {
            recepientItem.tags.RemoveAt(index);
        }
    }
}
