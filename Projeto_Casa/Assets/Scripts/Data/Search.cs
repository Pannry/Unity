﻿using System;
using System.Collections;
using UnityEngine;

namespace AssemblyCSharp
{
	public static class Search
	{
		//Busca em largura modificada, vai jogar no "result" todos os componentes que sejam quadros eletricos.
		public static void BreadthFirstSearch(ArrayList explored, ArrayList frontier, ArrayList result){
			if (frontier.Count > 0) {
				Node currentNode = (Node)(frontier [0]);
				frontier.Remove (currentNode);
				explored.Add (currentNode);
				if (currentNode.GetComponent<InfoQadroEletrico> () != null) {
					result.Add (currentNode);
				}
				foreach (Edge e in currentNode.GetEdges()) {
					Node myChild = null;
					if (!e.firstVertex.Equals (currentNode.gameObject))
						myChild = e.firstVertex.GetComponent<Node> ();
					else if (!e.secondVertex.Equals (currentNode.gameObject))
						myChild = e.secondVertex.GetComponent<Node> ();
					if (!explored.Contains (myChild)) {
						if (!frontier.Contains (myChild)) {
							frontier.Add (myChild);
						}
					}
				}
				BreadthFirstSearch (explored, frontier, result);
			}
		}
			
		public static bool MakeSearch(string objective,Node vertex){
			ArrayList frontier = new ArrayList ();
			frontier.Add (vertex);
			ArrayList explored = new ArrayList ();
			return BreadthFirstSearch (objective, explored, frontier);
		}
		public static bool BreadthFirstSearch(string objective, ArrayList explored, ArrayList frontier){
			//Debug.Log ("Busca");
			if (frontier.Count > 0) {
				Node currentNode = (Node)(frontier [0]);
				frontier.Remove (currentNode);
				explored.Add (currentNode);
				if (currentNode.GetComponent<InfoQadroEletrico> () != null) {
					InfoQadroEletrico aux = currentNode.GetComponent<InfoQadroEletrico> ();
					string myswitchboard = aux.GetID ().ToLower ().Trim ();
					//Debug.Log("Show me: " + objective.ToLower ().Trim () + " VS " + myswitchboard);
					if (objective.ToLower ().Trim () == myswitchboard)
						return true;
				}
				foreach (Edge e in currentNode.GetEdges()) {
					Node myChild = null;
					if (!e.firstVertex.Equals (currentNode.gameObject))
						myChild = e.firstVertex.GetComponent<Node> ();
					else if (!e.secondVertex.Equals (currentNode.gameObject))
						myChild = e.secondVertex.GetComponent<Node> ();
					if (!explored.Contains (myChild)) {
						if (myChild.GetComponent<InfoQadroEletrico> () != null) {
							InfoQadroEletrico aux = myChild.GetComponent<InfoQadroEletrico> ();
							string myswitchboard = aux.GetID ().ToLower ().Trim ();
							//Debug.Log("Show me: " + objective.ToLower ().Trim () + " VS " + myswitchboard);
							if (objective.ToLower ().Trim () == myswitchboard)
								return true;
						}
						if (!frontier.Contains (myChild)) {
							frontier.Add (myChild);
						}
					}
				}
				return BreadthFirstSearch (objective, explored, frontier);
			} else
				return false;
		}
	}
}

