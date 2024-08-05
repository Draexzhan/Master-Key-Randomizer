using System;

public class Class1
{/*
    public Transform OneCoin = { fileID: 4987245893405694, guid: 0e6988e912112d449b7ec38c28a13e9f, type: 2 }; //PetitePiece
    public Transform FiveCoin = { fileID: 4644969588541675, guid: c522c50063e76a1428ae1b3fa0fa812f, type: 2 }; //Piece5
    public Transform TenCoin = { fileID: 4128505307185942, guid: 9e547fb13e421874998bf17ca258af3b, type: 2 }; //GrossePiece10
    public Transform TwentyBucks = { fileID: 4638241284036108, guid: 895cb828dccddd845ad417af40fffc78, type: 2 }; //billet20
    public Transform GoldBars = { fileID: 4495140437891040, guid: 26f86c65f3bc7dc439c523e54c39d3f4, type: 2 }; //lingot
    public Transform Diamond = { fileID: 4293177538970759, guid: f9c889f5150fed7468c412816441a21c, type: 2 }; //diam100
    public Transform GearPiece1 = { fileID: 4347881985671189, , type: 2 }; //Gear1, FragmentGeatShop
    public Transform GearPiece2 = { fileID: 4567094537680644, guid: 3e0770765039e65468857eeb5744e61c, type: 2 }; //Gear2
    public Transform GearPiece3 = { fileID: 4039318283273796, guid: 1e3e0126e40c3ff4b8b2fc13b76da14f, type: 2 }; //Gear3
    public Transform Meat = { fileID: 4973759756143433, guid: ce8542794eb30d94d9f92d5cd0111f6b, type: 2 }; //REZ
    public Transform SuperMeat = { fileID: 4646268153279516, d697aad7f3391d042b7383e9515baa7a, type: 2 }; //REZ2
    public Transform Apple = { fileID: 4256458351558945, guid: 0e73ebdd518125e4291b8a008f30ed9b, type: 2 }; //Pomme
    public Transform Cheese = { fileID: 4849037220293278, guid: c13ee0ca04439304aae89884d07638fc, type: 2 }; //in OverWorld.unity
    public Transform TripleCrystal = { , , type: 2 }; //triCristal
    public Transform Crystal = { fileID: 4427300273841765, guid: c23277b71affdfa459ed595c4faba936, type: 2 }; //cristal
    public Transform HealthHalf = { fileID: 4714605705799210, guid: 0995911a7fe46c040a90190b6a586f53, type: 2 };//DemiCoeurDeVie, DemiCoeurDeVieCher
    public Transform HealthContainer = { fileID: 4839427619308025, guid: 0de3508662930e54b9d37a540d4e3b74, type: 2 }; //CoeurDeVie, CoeurDeVieCher
    public Transform OWMap = { fileID: 4367650442014721, guid: 8a6d57ec6d897f04ca0a02f7103c6f0b, type: 2 }; //MapOW
    public Transform SWMap = { fileID: 4038036866287753, guid: 8b732accc7a421048bf488f8aa394eb4, type: 2 };
    public Transform NEMap = { fileID: 4269666115791734, guid: 40838fff20251704383fd22bc7460b03, type: 2 };
    public Transform NWMap = { fileID: 4544409179596304, guid: b6f14d9a1f3e4f1489819840d4a34e6d, type: 2 };
    public Transform SEMap = { fileID: 4978329139570583, guid: 10816293ab229d846a45c637a469a7be, type: 2 };
    public Transform SecretMap = { fileID: 4062922319339255, guid: a0f46db4826a987438e14f5f63673491, type: 2 };
    public Transform ForgeMap = { fileID: 4998697027981107, guid: 8d5c12a597a1319429ee7f78c688a09a, type: 2 };
    public Transform FinalMap = { fileID: 4534362198642278, guid: 9966e970546b45347a79a19e293a5a29, type: 2 };
    public Transform SWKey = { fileID: 4379337264809527, guid: 0af948e1c11e5fd4e95d659b102f17d7, type: 2 };
    public Transform NWKey = { fileID: 4377721560542030, guid: 55151ab8b893a3b478b16c6647b70204, type: 2 };
    public Transform SEKey = { fileID: 4077862267318274, guid: c08fbef0668f06d46bd08739f5aa731a, type: 2 };
    public Transform SecretKey = { fileID: 4845972534179060, guid: f324b324a3cc4f549a792a6926cfb1d7, type: 2 };
    public Transform ForgeKey = { fileID: 4744126150306435, guid: aa15b188b83f94a4a8ffae84f641baeb, type: 2 };
    public Transform MasterKey = { , , type: 2 }; //ItemQuete
    public Transform BabySword = { , , type: 2 }; //smallSword
    public Transform Mace = { , , type: 2 }; //morgenStern (???)
    public Transform BigSword = { , , type: 2 }; //longSword
    public Transform BrokenSword = { , , type: 2 }; //BrokenMegaSword
    public Transform BusterSword = { fileID: 4294556169885175, guid: 5a570ac3b48d14244817ca1c77d34a87, type: 2 }; //MegaSword, UltimaSword
    public Transform Balloon = { fileID: 4664346508514034, guid: 36c19ea70ed98b44aa4a1c6d61681fcf, type: 2 }; //Ballon1
    public Transform Boomerang = { fileID: 4801416327874377, guid: 9d02fa5a8a6796b48a76dfa0fa9c2913, type: 2 }; //boomerang
    public Transform GrapplingHook = { fileID: 4467586986829986, guid: 55e314c56f8fec1468dfa7c2768659b9, type: 2 }; //grappin2
    public Transform Claws = { fileID: 4309739882359521, guid: 30f77bfce4b609644a6bc23fe78b305b, type: 2 }; //Gants
    public Transform Magnet = { fileID: 4738840676933056, guid: 3d5d7fdb03f8e69409625cf112f7ea15, type: 2 }; //Magnet
    public Transform Lantern = { fileID: 4030002299290021, guid: d0224a4eff8a4c8448192bc5788babee, type: 2 }; //lanterne1shopForet
    public Transform Vitamins = { , , type: 2 }; //Altères
    public Transform Luck = { fileID: 4610952718827451, guid: 24af0df925b378146b5123cb1fe4ab7c, type: 2 }; //clover
    public Transform Armor = { fileID: 4893429666393055, guid: dfdf5ae9896a67c42b03d5c7c03fb4bc, type: 2 }; //Armure
    public Transform Snorkel = { fileID: 4679547525939661, guid: 7541d4872948c114fa69a4498a2ad566, type: 2 }; //Tuba2
    public Transform Lens = { fileID: 4649442835429148, guid: 5ecfc6dfadc6723459dd1d3337cb05ad, type: 2 }; //loupe I think?
    //public Transform Lens2 = { fileID: 4963822068420238, guid: 0b8dd66578365844ba1cd24fdc83b808, type: 2 }; //loupe2 I think?
    public Transform Boots = { fileID: 4679294178075582, guid: b01661cabe4e71040b9edd9ac3878f61, type: 2 }; //Bottes
    public Transform Wallet = { , , type: 2 }; //MoyenneBourse
    public Transform BigWallet = { fileID: 4570125662528609, guid: 31fee63b1958caa43803750b159a9c87, type: 2 }; //GrandeBourse999
    public Transform MegaWallet = { , , type: 2 }; //GrandeBourse999-2
    public Transform QuestKeySW = { fileID: 4719897291241875, guid: 42682ab961f412e4b9099e7b422b7887, type: 2 };
    public Transform QuestKeyNE = { fileID: 4781566831381263, guid: 872534307e2bfa748ac29744201db3e8, type: 2 };
    public Transform QuestKeyNW = { fileID: 4767296479664427, guid: d9a894140dcfc8f45b38326e44e6fdcd, type: 2 };
    public Transform QuestKeySE = { fileID: 4396960834283724, guid: 0c8fa857abcd57144bc91c1f68e74646, type: 2 };
    public Transform QuestCrestSW = { fileID: 4743463564056841, guid: d0a5ed35a7a84db4a8ba04633aa04e70, type: 2 }; //Fragment1
    public Transform QuestCrestNE = { fileID: 4723381906873389, guid: f1314343ce8194c489854a6c31b7c981, type: 2 }; //Fragment2
    public Transform QuestCrestNW = { fileID: 4551014241233300, guid: 8298a647fe1d17e4b8b58a42c6f77bcd, type: 2 }; //Fragment3
    public Transform QuestCrestSE = { fileID: 4067307864464732, guid: 6952709964d5f494e8ba19931fe9c42a, type: 2 }; //Fragment4
    public Transform Record = { , , type: 2 };
    public Transform PellyJunk = { , , type: 2 };
    public Transform PellyNice = { , , type: 2 };
    public Transform PellyRare = { , , type: 2 };
    public Transform DarkOre = { fileID: 4998467096068804, guid: a6bc4f62ed3c8ff47bdc14ee0b97ad33, type: 2 };
    public Transform ShitSword = { , , type: 2 };

    public Class1()
	{
	}*/
}
