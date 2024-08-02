using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolicyList : MonoBehaviour
{
   /// <summary>
   /// -- C.Love -- this is a dictionary of all policies I have edited some values based off of ChatGPTs initial output
   /// </summary>

    public Dictionary<string, Dictionary<string, float>> policies = new Dictionary<string, Dictionary<string, float>>
    {
        { "NATIONAL HEALTH SERVICE FUNDING _ funding to support the NHS and healthcare services _ DEPARTMENT OF HEALTH & SOCIAL CARE",
            new Dictionary<string, float>
            {
                { "youngImpact", 60.0f },
                { "profImpact", 70.0f },
                { "oldImpact", 40.0f },
                { "lowImpact", 50.0f },
                { "intermediateImpact", 60.0f },
                { "highImpact", 30.0f },
                { "investmentImpact", -80.0f }
            }
        },
        { "PUBLIC EDUCATION FUNDING _ financial support for state education systems _ DEPARTMENT FOR EDUCATION",
            new Dictionary<string, float>
            {
                { "youngImpact", 50.0f },
                { "profImpact", 40.0f },
                { "oldImpact", 30.0f },
                { "lowImpact", 40.0f },
                { "intermediateImpact", 50.0f },
                { "highImpact", 60.0f },
                { "investmentImpact", -20.0f }
            }
        },
        { "TAXATION POLICIES _ changes to tax laws and rates _ HM TREASURY",
            new Dictionary<string, float>
            {
                { "youngImpact", 40.0f },
                { "profImpact", 50.0f },
                { "oldImpact", 60.0f },
                { "lowImpact", 50.0f },
                { "intermediateImpact", 40.0f },
                { "highImpact", 30.0f },
                { "investmentImpact", -30.0f }
            }
        },
        { "TRANSPORT INFRASTRUCTURE INVESTMENT _ funding for roads, railways, and public transport _ DEPARTMENT FOR TRANSPORT",
            new Dictionary<string, float>
            {
                { "youngImpact", 70.0f },
                { "profImpact", 60.0f },
                { "oldImpact", 40.0f },
                { "lowImpact", 50.0f },
                { "intermediateImpact", 60.0f },
                { "highImpact", 80.0f },
                { "investmentImpact", -30.0f }
            }
        },
        { "MENTAL HEALTH SERVICES EXPANSION _ improving access to mental health care _ DEPARTMENT OF HEALTH AND SOCIAL CARE",
            new Dictionary<string, float>
            {
                { "youngImpact", 60.0f },
                { "profImpact", 70.0f },
                { "oldImpact", 40.0f },
                { "lowImpact", 50.0f },
                { "intermediateImpact", 60.0f },
                { "highImpact", 30.0f },
                { "investmentImpact", -20.0f }
            }
        },
        { "RESEARCH GRANTS _ incentives to promote scientific innovation _ DEPARTMENT FOR SCIENCE INNOVATION & TECHNOLOGY",
            new Dictionary<string, float>
            {
                { "youngImpact", 50.0f },
                { "profImpact", 40.0f },
                { "oldImpact", 30.0f },
                { "lowImpact", 40.0f },
                { "intermediateImpact", 50.0f },
                { "highImpact", 60.0f },
                { "investmentImpact", 50.0f }
            }
        },
        { "SOCIAL HOUSING INITIATIVES _ programmes to increase affordable housing availability _ MINISTRY OF HOUSING & LOCAL GOVERNMENT",
            new Dictionary<string, float>
            {
                { "youngImpact", 50.0f },
                { "profImpact", 40.0f },
                { "oldImpact", 30.0f },
                { "lowImpact", 40.0f },
                { "intermediateImpact", 50.0f },
                { "highImpact", 60.0f },
                { "investmentImpact", -30.0f }
            }
        },
        { "ECONOMIC STIMULUS PACKAGES _ measures to boost economic growth _ HM TREASURY",
            new Dictionary<string, float>
            {
                { "youngImpact", 40.0f },
                { "profImpact", 50.0f },
                { "oldImpact", 60.0f },
                { "lowImpact", 50.0f },
                { "intermediateImpact", 40.0f },
                { "highImpact", 30.0f },
                { "investmentImpact", 10.0f }
            }
        },
        { "EMPLOYMENT RIGHTS LEGISLATION _ laws protecting workers' rights _ DEPARTMENT FOR WORK & PENSIONS",
            new Dictionary<string, float>
            {
                { "youngImpact", 60.0f },
                { "profImpact", 70.0f },
                { "oldImpact", 40.0f },
                { "lowImpact", 50.0f },
                { "intermediateImpact", 60.0f },
                { "highImpact", 30.0f },
                { "investmentImpact", -50.0f }
            }
        },
        { "DIGITAL INFRASTRUCTURE DEVELOPMENT _ investment in digital connectivity _ DEPARTMENT FOR SCIENCE INNOVATION & TECHNOLOGY",
            new Dictionary<string, float>
            {
                { "youngImpact", 70.0f },
                { "profImpact", 60.0f },
                { "oldImpact", 10.0f },
                { "lowImpact", 60.0f },
                { "intermediateImpact", 40.0f },
                { "highImpact", 20.0f },
                { "investmentImpact", 20.0f }
            }
        },
        { "ENVIRONMENTAL REGULATIONS _ laws to protect natural resources and green spaces _ ENVIRONMENT FOOD & RURAL AFFAIRS",
            new Dictionary<string, float>
            {
                { "youngImpact", 50.0f },
                { "profImpact", 40.0f },
                { "oldImpact", 50.0f },
                { "lowImpact", 60.0f },
                { "intermediateImpact", 30.0f },
                { "highImpact", 20.0f },
                { "investmentImpact", -70f }
            }
        },
        { "ANTI-DISCRIMINATION LEGISLATION _ laws to prevent discrimination based on race, gender, etc. _ GOVERNMENT EQUALITIES OFFICE",
            new Dictionary<string, float>
            {
                { "youngImpact", 60.0f },
                { "profImpact", 70.0f },
                { "oldImpact", -10f },
                { "lowImpact", 10f },
                { "intermediateImpact", 60.0f },
                { "highImpact", 30.0f },
                { "investmentImpact", -30.0f }
            }
        },
        { "PENSION REFORM _ changes to retirement age _ DEPARTMENT FOR WORK & PENSIONS",
            new Dictionary<string, float>
            {
                { "youngImpact", -30f },
                { "profImpact", -20f },
                { "oldImpact", 0f },
                { "lowImpact", -40f },
                { "intermediateImpact", -10f },
                { "highImpact", 30f },
                { "investmentImpact", 50f }
            }
        },
        { "DEVELOPMENT AID _ financial support for developing countries _ FOREIGN OFFICE",
            new Dictionary<string, float>
            {
                { "youngImpact", 30.0f },
                { "profImpact", 40.0f },
                { "oldImpact", 50.0f },
                { "lowImpact", 60.0f },
                { "intermediateImpact", 30.0f },
                { "highImpact", 20.0f },
                { "investmentImpact", -10.0f }
            }
        },
        { "REGULATION OF FINANCIAL MARKETS _ enforcing rules governing financial institutions _ DEPARTMENT FOR BUSINESS & TRADE",
            new Dictionary<string, float>
            {
                { "youngImpact", 60.0f },
                { "profImpact", 70.0f },
                { "oldImpact", 40.0f },
                { "lowImpact", 60.0f },
                { "intermediateImpact", 0f },
                { "highImpact", -30f },
                { "investmentImpact", -70f }
            }
        },
        { "ENERGY EFFICIENCY INITIATIVES _ programmes to reduce energy consumption _ DEPARTMENT FOR ENERGY SECURITY & NET ZERO",
            new Dictionary<string, float>
            {
                { "youngImpact", 60.0f },
                { "profImpact", 70.0f },
                { "oldImpact", 40.0f },
                { "lowImpact", 50.0f },
                { "intermediateImpact", 60.0f },
                { "highImpact", 30.0f },
                { "investmentImpact", -20.0f }
            }
        },
        { "FOREIGN TRADE AGREEMENTS _ deals to facilitate international trade _ FOREIGN OFFICE",
            new Dictionary<string, float>
            {
                { "youngImpact", 50.0f },
                { "profImpact", 40.0f },
                { "oldImpact", 30.0f },
                { "lowImpact", 40.0f },
                { "intermediateImpact", 50.0f },
                { "highImpact", 60.0f },
                { "investmentImpact", 70.0f }
            }
        },
        { "PUBLIC TRANSPORTATION FUNDING _ support for buses, trains, and other public transit _ DEPARTMENT FOR TRANSPORT",
            new Dictionary<string, float>
            {
                { "youngImpact", 70.0f },
                { "profImpact", 60.0f },
                { "oldImpact", 40.0f },
                { "lowImpact", 50.0f },
                { "intermediateImpact", 60.0f },
                { "highImpact", 80.0f },
                { "investmentImpact", -50.0f }
            }
        },
            { "ANTI-MIGRATION POLICIES _ stricter immigration control measures _ HOME OFFICE", 
        new Dictionary<string, float>
        {
            { "youngImpact", -20.0f },
            { "profImpact", -10.0f },
            { "oldImpact", 50.0f },
            { "lowImpact", 30.0f },
            { "intermediateImpact", -10.0f },
            { "highImpact", -20.0f },
            { "investmentImpact", -50.0f }
        }
    },
    { "REJOINING THE EUROPEAN UNION _ reversing Brexit and rejoining the EU _ FOREIGN OFFICE", 
        new Dictionary<string, float>
        {
            { "youngImpact", 70.0f },
            { "profImpact", 50.0f },
            { "oldImpact", -40.0f },
            { "lowImpact", -30.0f },
            { "intermediateImpact", 40.0f },
            { "highImpact", 10.0f },
            { "investmentImpact", 50.0f }
        }
    },
    { "MANDATORY MILITARY SERVICE _ compulsory conscription for all citizens _ MINISTRY OF DEFENCE", 
        new Dictionary<string, float>
        {
            { "youngImpact", -50.0f },
            { "profImpact", -20.0f },
            { "oldImpact", 30.0f },
            { "lowImpact", 20.0f },
            { "intermediateImpact", -30.0f },
            { "highImpact", -40.0f },
            { "investmentImpact", -10.0f }
        }
    },
    { "TAX ON DIGITAL SERVICES _ imposing taxes on tech giants _ DEPARTMENT FOR SCIENCE INNOVATION & TECHNOLOGY", 
        new Dictionary<string, float>
        {
            { "youngImpact", 50f },
            { "profImpact", 30.0f },
            { "oldImpact", 20.0f },
            { "lowImpact", 30.0f },
            { "intermediateImpact", -10.0f },
            { "highImpact", -20.0f },
            { "investmentImpact", -70.0f }
        }
    },
    { "PROHIBITION ON SINGLE-USE PLASTICS _ banning plastic straws, bags, and utensils _ ENVIRONMENT FOOD AND RURAL AFFAIRS", 
        new Dictionary<string, float>
        {
            { "youngImpact", 50.0f },
            { "profImpact", 30.0f },
            { "oldImpact", -10.0f },
            { "lowImpact", -20.0f },
            { "intermediateImpact", 40.0f },
            { "highImpact", 20.0f },
            { "investmentImpact", -30.0f }
        }
    }
    };



    

    public string GetRandomPolicy()
    {
        List<string> keys = new List<string>(policies.Keys);
        string randomKey = keys[UnityEngine.Random.Range(0, keys.Count)];
        return randomKey;
    }

    public Dictionary<string,float> GetPolicyValues(string x)
    {
        return policies[x];
    }

}
